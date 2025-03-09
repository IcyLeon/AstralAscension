using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static InventoryManager;

public class SlotPopup : MonoBehaviour
{
    [SerializeField] private GameObject MainPanel;
    private ItemManagerSO ItemManagerSO;
    [SerializeField] private ScrollRect ScrollRect;
    [SerializeField] private ItemCard itemCard;
    private SlotManager slotManager;

    private Dictionary<ItemFamilyTypeSO, Dictionary<IItem, ItemQualityIEntity>> itemQualityDictionary;
    private List<ItemQualityButton> sortedEntities;
    private IItem iItem;
    private Inventory inventory;
    public event Action<Slot> OnSlotChanged;
    public event Action<Slot> OnSlotItemAdd;

    private void Awake()
    {
        itemQualityDictionary = new();
        sortedEntities = new();
        OnInventoryOld += InventoryManager_OnInventoryOld;
        OnInventoryNew += InventoryManager_OnInventoryNew;
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

    public void SetIItem(IItem IItem)
    {
        iItem = IItem;
        slotManager.SetIItem(iItem);
        UpdateSlotContent();
    }

    private void UpdateSlotContent()
    {
        if (iItem == null)
            return;

        ItemFamilyTypeSO itemFamilyTypeSO = iItem.GetTypeSO().ItemFamilyTypeSO;

        if (!itemQualityDictionary.TryGetValue(itemFamilyTypeSO, out Dictionary<IItem, ItemQualityIEntity> itemQualityDic))
            return;

        foreach(var ItemQualityIEntity in itemQualityDic.Values)
        {
            ItemQualityIEntity.gameObject.SetActive(ItemQualityIEntity.ItemQuality.iItem != iItem && !IsEquippedByCharacter(ItemQualityIEntity.ItemQuality.iItem));
        }
    }


    private void Start()
    {
        Init();
    }

    private void Init()
    {
        if (instance == null)
        {
            Debug.Log("Inventory Manager not Found!");
            return;
        }

        ItemManagerSO = AssetManager.instance.ItemAssetManagerSO;
        InventoryManager_OnInventoryNew(instance.inventory);
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

    private void InventoryManager_OnInventoryOld(Inventory Inventory)
    {
        Inventory.OnItemAdd -= Inventory_OnItemAdd;
        Inventory.OnItemRemove -= Inventory_OnItemRemove;
    }

    private void InventoryManager_OnInventoryNew(Inventory Inventory)
    {
        if (inventory == Inventory)
            return;

        inventory = Inventory;

        if (inventory != null)
        {
            inventory.OnItemAdd += Inventory_OnItemAdd;
            inventory.OnItemRemove += Inventory_OnItemRemove;
            UpdateVisual();
        }
    }

    private void Inventory_OnItemRemove(Item Item)
    {
        ItemQualityIEntity itemQualityIEntity = RemoveItemQualityFromDictionary(Item);

        if (itemQualityIEntity == null)
            return;

        itemQualityIEntity.ItemQualitySelection.OnRemoveClick -= ItemQualitySelection_OnRemoveClick;
        itemQualityIEntity.Destroy();
    }

    private bool IsEquippedByCharacter(IItem IItem)
    {
        UpgradableItems upgradableItems = IItem as UpgradableItems;

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
        ItemFamilyTypeSO itemFamilyTypeSO = ItemQualityIEntity.ItemQuality.iItem.GetTypeSO().ItemFamilyTypeSO;

        if (!itemQualityDictionary.ContainsKey(itemFamilyTypeSO))
        {
            itemQualityDictionary.Add(itemFamilyTypeSO, new());
        }

        itemQualityDictionary[itemFamilyTypeSO].Add(ItemQualityIEntity.ItemQuality.iItem, ItemQualityIEntity);
        AddSorted(ItemQualityIEntity);
        UpdateSlotContent();
    }

    private ItemQualityIEntity RemoveItemQualityFromDictionary(IItem IItem)
    {
        ItemFamilyTypeSO itemFamilyTypeSO = IItem.GetTypeSO().ItemFamilyTypeSO;

        if (!itemQualityDictionary.TryGetValue(itemFamilyTypeSO, out Dictionary<IItem, ItemQualityIEntity> itemQualityDic))
            return null;

        ItemQualityIEntity itemQualityIEntity = itemQualityDic[IItem];
        itemQualityDic.Remove(IItem);
        sortedEntities.Remove(itemQualityIEntity);

        if (itemQualityDic.Count == 0)
        {
            itemQualityDictionary.Remove(itemFamilyTypeSO);
        }

        UpdateSlotContent();

        return itemQualityIEntity;
    }


    private void AddSorted(ItemQualityButton ItemQualityButton)
    {
        int location = GetLocation(ItemQualityButton, 0, sortedEntities.Count - 1);
        sortedEntities.Insert(location, ItemQualityButton);
        ItemQualityButton.transform.SetSiblingIndex(location);
    }
    private int GetLocation(ItemQualityButton ItemQualityButton, int start, int end)
    {
        if (start > end)
            return start;

        int mid = (start + end) / 2;

        IItem IMidEntity = sortedEntities[mid].ItemQuality.iItem;

        int nameComparison = string.Compare(IMidEntity.GetName(), ItemQualityButton.ItemQuality.iItem.GetName(), StringComparison.OrdinalIgnoreCase);

        if (IMidEntity.GetRaritySO().Rarity < ItemQualityButton.ItemQuality.iItem.GetRaritySO().Rarity)
        {
            return GetLocation(ItemQualityButton, start, mid - 1);
        }
        else if (IMidEntity.GetRaritySO().Rarity > ItemQualityButton.ItemQuality.iItem.GetRaritySO().Rarity)
        {
            return GetLocation(ItemQualityButton, mid + 1, end);
        }
        else
        {
            if (nameComparison < 0)
            {
                return GetLocation(ItemQualityButton, mid + 1, end);
            }
            else if (nameComparison > 0)
            {
                return GetLocation(ItemQualityButton, start, mid - 1);
            }
            else
            {
                return mid;
            }
        }
    }

    public List<IItem> GetEntityList(Rarity MaxRarity, int amount)
    {
        List<IItem> entitiesList = new();

        for (int i = 0; i < amount; i++)
        {
            int index = sortedEntities.Count - i - 1;

            if (index < 0)
                break;

            ItemQualityButton ItemQualityButton = sortedEntities[index];

            if (!ItemQualityButton.gameObject.activeSelf || ItemQualityButton.ItemQuality.iItem.GetRaritySO().Rarity > MaxRarity ||
                !slotManager.CanManualAdd(ItemQualityButton.ItemQuality.iItem))
            {
                amount++;
                continue;
            }

            entitiesList.Add(ItemQualityButton.ItemQuality.iItem);
        }

        return entitiesList;
    }

    private void ItemQualitySelection_OnRemoveClick(ItemQualityButton ItemQualityButton)
    {
        Slot slot = slotManager.Contains(GetIItem(ItemQualityButton));

        if (slot != null)
        {
            slot.DeleteIItem();
        }
    }

    private void SlotManager_OnSlotItemAdd(Slot Slot)
    {
        IItem iItem = GetIItem(Slot.itemQualityButton);

        RevealItemCard(iItem);

        ItemQualityIEntity ItemQualityIEntity = GetItemQualityIItem(iItem);

        if (ItemQualityIEntity == null)
            return;

        ItemQualityIEntity.ItemQualitySelection.Select();
        OnSlotItemAdd?.Invoke(Slot);
    }

    private void SlotManager_OnSlotItemRemove(IItem IItem, Slot Slot)
    {
        ItemQualityIEntity ItemQualityIEntity = GetItemQualityIItem(IItem);

        if (ItemQualityIEntity == null)
            return;

        ItemQualityIEntity.ItemQualitySelection.UnSelect();
    }


    private void SlotManager_OnSlotSelected(Slot Slot)
    {
        IItem iItem = GetIItem(Slot.itemQualityButton);

        RevealItemCard(iItem);
    }

    private ItemQualityIEntity GetItemQualityIItem(IItem IItem)
    {
        ItemFamilyTypeSO itemFamilyTypeSO = IItem.GetTypeSO().ItemFamilyTypeSO;

        if (!itemQualityDictionary.TryGetValue(itemFamilyTypeSO, out Dictionary<IItem, ItemQualityIEntity> itemQualityDic))
            return null;

        if (!itemQualityDic.TryGetValue(IItem, out ItemQualityIEntity ItemQualityIEntity))
            return null;

        return ItemQualityIEntity;
    }

    private void RevealItemCard(IItem IItem)
    {
        MainPanel.SetActive(true);
        itemCard.SetIItem(IItem);
    }

    private void OnSelectedItemQualityButton(ItemQualityButton ItemQualityButton)
    {
        IItem iItem = GetIItem(ItemQualityButton);

        RevealItemCard(iItem);

        if (!slotManager.CanManualAdd(iItem))
            return;

        AddEntityToSlot(iItem);
    }

    public bool AddEntityToSlot(IItem IItem)
    {
        return slotManager.TryAddEntityToSlot(IItem);
    }

    private IItem GetIItem(ItemQualityButton ItemQualityButton)
    {
        if (ItemQualityButton == null)
        {
            return null;
        }

        return ItemQualityButton.ItemQuality.iItem;
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

        OnInventoryOld -= InventoryManager_OnInventoryOld;
        OnInventoryNew -= InventoryManager_OnInventoryNew;

        if (inventory != null)
        {
            inventory.OnItemAdd -= Inventory_OnItemAdd;
            inventory.OnItemRemove -= Inventory_OnItemRemove;
        }
    }
}
