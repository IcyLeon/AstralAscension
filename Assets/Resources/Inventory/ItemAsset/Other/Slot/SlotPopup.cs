using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SlotPopup : MonoBehaviour
{
    [SerializeField] private GameObject MainPanel;
    private ItemManagerSO ItemManagerSO;
    [SerializeField] private ScrollRect ScrollRect;
    [SerializeField] private ItemCard itemCard;
    private SlotManager slotManager;

    private Dictionary<ItemFamilyTypeSO, Dictionary<IData, ItemQualityIEntity>> itemQualityDictionary = new();
    private List<ItemQuality> sortedEntities;
    private IData iData;
    private Inventory inventory;
    public event Action<Slot> OnSlotChanged;
    public event Action<Slot> OnSlotItemAdd;

    private void Awake()
    {
        sortedEntities = new();
        InventoryManager.OnInventoryChanged += InventoryManager_OnInventoryChanged;
    }

    public void SetSlotManager(SlotManager SlotManager)
    {
        if (slotManager != null)
            return;

        slotManager = SlotManager;
        slotManager.OnSlotChanged += SlotManager_OnSlotChanged;
        slotManager.OnSlotSelected += SlotManager_OnSlotSelected;
        slotManager.OnSlotItemAdd += SlotManager_OnSlotItemAdd;
        slotManager.OnSlotItemRemove += SlotManager_OnSlotItemRemove;
    }

    private void SlotManager_OnSlotChanged(Slot Slot)
    {
        OnSlotChanged?.Invoke(Slot);
    }

    public void SetIItem(IData IData)
    {
        iData = IData;
        slotManager.SetIItem(iData);
        UpdateSlotContent();
    }

    private void UpdateSlotContent()
    {
        if (iData == null)
            return;

        ItemFamilyTypeSO itemFamilyTypeSO = iData.GetTypeSO().ItemFamilyTypeSO;

        if (!itemQualityDictionary.TryGetValue(itemFamilyTypeSO, out Dictionary<IData, ItemQualityIEntity> itemQualityDic))
            return;

        foreach(var ItemQualityIEntity in itemQualityDic.Values)
        {
            ItemQualityIEntity.gameObject.SetActive(ItemQualityIEntity.iData != iData && !IsEquippedByCharacter(ItemQualityIEntity.iData));
        }
    }


    private void Start()
    {
        Init();
    }

    private void Init()
    {
        if (InventoryManager.instance == null)
        {
            Debug.Log("Inventory Manager not Found!");
            return;
        }

        ItemManagerSO = AssetManager.instance.ItemAssetManagerSO;
        InventoryManager_OnInventoryChanged(InventoryManager.instance.inventory);
    }

    public void RemoveItems(List<IEntity> ItemList)
    {
        if (inventory == null)
            return;

        for (int i = ItemList.Count - 1; i >= 0; i--)
        {
            inventory.RemoveItem(ItemList[i] as Item);
        }
    }

    private void InventoryManager_OnInventoryChanged(Inventory Inventory)
    {
        if (inventory == Inventory)
            return;

        if (inventory != null)
        {
            Inventory.OnItemAdd -= Inventory_OnItemAdd;
            Inventory.OnItemRemove -= Inventory_OnItemRemove;
        }

        inventory = Inventory;

        inventory.OnItemAdd += Inventory_OnItemAdd;
        inventory.OnItemRemove += Inventory_OnItemRemove;
        UpdateVisual();
    }

    private void Inventory_OnItemRemove(Item Item)
    {
        ItemQualityIEntity itemQualityIEntity = RemoveItemQualityFromDictionary(Item);

        if (itemQualityIEntity == null)
            return;

        itemQualityIEntity.ItemQualitySelection.OnRemoveClick -= ItemQualitySelection_OnRemoveClick;
        Destroy(itemQualityIEntity.gameObject);
    }

    private bool IsEquippedByCharacter(IData iData)
    {
        UpgradableItems upgradableItems = iData as UpgradableItems;

        if (upgradableItems == null)
            return false;

        return upgradableItems.equipByCharacter != null;
    }

    public List<IEntity> GetAllSlotEntities()
    {
        return slotManager.GetAllSlotEntities();
    }

    private void Inventory_OnItemAdd(Item Item)
    {
        ItemQualityIEntity itemQualityIEntity = ItemManagerSO.CreateItemQualityItem(Item, ScrollRect.content);
        itemQualityIEntity.OnItemQualitySelect += OnSelectedItemQualityButton;
        itemQualityIEntity.ItemQualitySelection.OnRemoveClick += ItemQualitySelection_OnRemoveClick;
        itemQualityIEntity.gameObject.SetActive(false);
        AddItemQualityToDictionary(itemQualityIEntity);
    }

    private void AddItemQualityToDictionary(ItemQualityIEntity ItemQualityIEntity)
    {
        ItemFamilyTypeSO itemFamilyTypeSO = ItemQualityIEntity.iData.GetTypeSO().ItemFamilyTypeSO;

        if (!itemQualityDictionary.ContainsKey(itemFamilyTypeSO))
        {
            itemQualityDictionary.Add(itemFamilyTypeSO, new());
        }

        itemQualityDictionary[itemFamilyTypeSO].Add(ItemQualityIEntity.iData, ItemQualityIEntity);
        AddSorted(ItemQualityIEntity);
        UpdateSlotContent();
    }

    private ItemQualityIEntity RemoveItemQualityFromDictionary(IData iData)
    {
        ItemFamilyTypeSO itemFamilyTypeSO = iData.GetTypeSO().ItemFamilyTypeSO;

        if (!itemQualityDictionary.TryGetValue(itemFamilyTypeSO, out Dictionary<IData, ItemQualityIEntity> itemQualityDic))
            return null;

        ItemQualityIEntity itemQualityIEntity = itemQualityDic[iData];
        itemQualityDic.Remove(iData);
        sortedEntities.Remove(itemQualityIEntity);

        if (itemQualityDic.Count == 0)
        {
            itemQualityDictionary.Remove(itemFamilyTypeSO);
        }

        UpdateSlotContent();

        return itemQualityIEntity;
    }


    private void AddSorted(ItemQuality itemQuality)
    {
        int location = GetLocation(itemQuality, 0, sortedEntities.Count - 1);
        sortedEntities.Insert(location, itemQuality);
        itemQuality.transform.SetSiblingIndex(location);
    }
    private int GetLocation(ItemQuality itemQuality, int start, int end)
    {
        if (start > end)
            return start;

        int mid = (start + end) / 2;

        IData IMidEntity = sortedEntities[mid].iData;

        int nameComparison = string.Compare(IMidEntity.GetName(), itemQuality.iData.GetName(), StringComparison.OrdinalIgnoreCase);

        if (IMidEntity.GetRaritySO().Rarity < itemQuality.iData.GetRaritySO().Rarity)
        {
            return GetLocation(itemQuality, start, mid - 1);
        }
        else if (IMidEntity.GetRaritySO().Rarity > itemQuality.iData.GetRaritySO().Rarity)
        {
            return GetLocation(itemQuality, mid + 1, end);
        }
        else
        {
            if (nameComparison < 0)
            {
                return GetLocation(itemQuality, mid + 1, end);
            }
            else if (nameComparison > 0)
            {
                return GetLocation(itemQuality, start, mid - 1);
            }
            else
            {
                return mid;
            }
        }
    }

    public List<IData> GetEntityList(Rarity MaxRarity, int amount)
    {
        List<IData> entitiesList = new();

        for (int i = 0; i < amount; i++)
        {
            int index = sortedEntities.Count - i - 1;

            if (index < 0)
                break;

            ItemQuality itemQuality = sortedEntities[index];

            if (!itemQuality.gameObject.activeSelf || itemQuality.iData.GetRaritySO().Rarity > MaxRarity ||
                !slotManager.CanManualAdd(itemQuality.iData))
            {
                amount++;
                continue;
            }

            entitiesList.Add(itemQuality.iData);
        }

        return entitiesList;
    }

    private void ItemQualitySelection_OnRemoveClick(ItemQuality itemQuality)
    {
        Slot slot = slotManager.Contains(GetIItem(itemQuality));

        if (slot != null)
        {
            slot.DeleteIItem();
        }
    }

    private void SlotManager_OnSlotItemAdd(Slot Slot)
    {
        IData iData = GetIItem(Slot.itemQuality);

        RevealItemCard(iData);

        ItemQualityIEntity ItemQualityIEntity = GetItemQualityIItem(iData);

        if (ItemQualityIEntity == null)
            return;

        ItemQualityIEntity.ItemQualitySelection.Select();
        OnSlotItemAdd?.Invoke(Slot);
    }

    private void SlotManager_OnSlotItemRemove(IData iData, Slot Slot)
    {
        ItemQualityIEntity ItemQualityIEntity = GetItemQualityIItem(iData);

        if (ItemQualityIEntity == null)
            return;

        ItemQualityIEntity.ItemQualitySelection.UnSelect();
    }


    private void SlotManager_OnSlotSelected(Slot Slot)
    {
        IData iData = GetIItem(Slot.itemQuality);

        RevealItemCard(iData);
    }

    private ItemQualityIEntity GetItemQualityIItem(IData iData)
    {
        ItemFamilyTypeSO itemFamilyTypeSO = iData.GetTypeSO().ItemFamilyTypeSO;

        if (!itemQualityDictionary.TryGetValue(itemFamilyTypeSO, out Dictionary<IData, ItemQualityIEntity> itemQualityDic))
            return null;

        if (!itemQualityDic.TryGetValue(iData, out ItemQualityIEntity ItemQualityIEntity))
            return null;

        return ItemQualityIEntity;
    }

    private void RevealItemCard(IData iData)
    {
        MainPanel.SetActive(true);
        itemCard.SetIItem(iData);
    }

    private void OnSelectedItemQualityButton(ItemQuality itemQuality)
    {
        IData iData = GetIItem(itemQuality);

        RevealItemCard(iData);

        if (!slotManager.CanManualAdd(iData))
            return;

        AddEntityToSlot(iData);
    }

    public bool AddEntityToSlot(IData iData)
    {
        return slotManager.TryAddEntityToSlot(iData);
    }

    private IData GetIItem(ItemQuality itemQuality)
    {
        if (itemQuality == null)
        {
            return null;
        }

        return itemQuality.iData;
    }

    private void UpdateVisual()
    {
        foreach(Transform t in ScrollRect.content)
            Destroy(t.gameObject);

        itemQualityDictionary.Clear();

        foreach (var item in inventory.itemList)
        {
            Inventory_OnItemAdd(item.Key);
        }
    }

    private void OnDestroy()
    {
        slotManager.OnSlotChanged -= SlotManager_OnSlotChanged;
        slotManager.OnSlotSelected -= SlotManager_OnSlotSelected;
        slotManager.OnSlotItemAdd -= SlotManager_OnSlotItemAdd;
        slotManager.OnSlotItemRemove -= SlotManager_OnSlotItemRemove;

        InventoryManager.OnInventoryChanged -= InventoryManager_OnInventoryChanged;

        if (inventory != null)
        {
            inventory.OnItemAdd -= Inventory_OnItemAdd;
            inventory.OnItemRemove -= Inventory_OnItemRemove;
        }
    }
}
