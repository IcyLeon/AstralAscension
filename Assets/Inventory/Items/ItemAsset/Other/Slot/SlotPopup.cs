using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static InventoryManager;

public class SlotPopup : MonoBehaviour
{
    private ObjectPool<ItemQualityIEntity> itemQualityPool;
    [SerializeField] private GameObject MainPanel;
    private ItemManagerSO ItemManagerSO;
    [SerializeField] private ScrollRect ScrollRect;
    [SerializeField] private ItemCard itemCard;
    [field: SerializeField] public SlotManager slotManager { get; private set; }

    private Dictionary<IItem, ItemQualityIEntity> itemQualityDictionary;
    public List<IItem> sortedEntities { get; private set; }
    private IItem iItem;
    private Inventory inventory;
    public event Action<Slot> OnSlotChanged;

    private void Awake()
    {
        itemQualityDictionary = new();
        sortedEntities = new();
        OnInventoryOld += InventoryManager_OnInventoryOld;
        OnInventoryNew += InventoryManager_OnInventoryNew;
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
        UpdateVisual();
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        ItemManagerSO = AssetManager.instance.ItemAssetManagerSO;
        //itemQualityPool = ItemManagerSO.CreateItemQualityItem(ScrollRect.content, 1000);

        if (instance == null)
        {
            Debug.Log("Inventory Manager not Found!");
            return;
        }

        InventoryManager_OnInventoryNew(instance.inventory);
    }

    public void RemoveItems(List<IEntity> ItemEntityList)
    {
        if (inventory == null)
            return;

        for (int i = ItemEntityList.Count - 1; i >= 0; i--)
        {
            inventory.RemoveItem(ItemEntityList[i]);
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

    private void Inventory_OnItemRemove(IItem IItem)
    {
        if (!itemQualityDictionary.TryGetValue(IItem, out ItemQualityIEntity itemQualityIEntity))
            return;

        //itemQualityIEntity.OnItemQualityClick -= OnSelectedItemQualityButton;
        itemQualityIEntity.ItemQualitySelection.OnRemoveClick -= OnItemQualityRemove;
        //Destroy(itemQualityIEntity.gameObject);
        itemQualityIEntity.Destroy();
        //itemQualityPool.Reset(itemQualityIEntity);
        itemQualityDictionary.Remove(IItem);
        sortedEntities.Remove(IItem);
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

    private void Inventory_OnItemAdd(IItem IItem)
    {
        if (IItem == iItem || (iItem != null &&
            (iItem.GetIItem().GetTypeSO().ItemFamilyTypeSO != IItem.GetIItem().GetTypeSO().ItemFamilyTypeSO ||
            IsEquippedByCharacter(IItem))))
            return;

        //ItemQualityIEntity itemQualityIEntity = itemQualityPool.GetPooledObject();
        //itemQualityIEntity.SetIItem(IItem);
        ItemQualityIEntity itemQualityIEntity = ItemManagerSO.CreateItemQualityItem(IItem, ScrollRect.content);
        itemQualityIEntity.OnItemQualitySelect += OnSelectedItemQualityButton;
        itemQualityIEntity.ItemQualitySelection.OnRemoveClick += OnItemQualityRemove;
        itemQualityDictionary.Add(IItem, itemQualityIEntity);
        AddSorted(itemQualityIEntity);
    }

    private void AddSorted(ItemQualityIEntity ItemQualityIEntity)
    {
        int location = GetLocation(ItemQualityIEntity.iEntity, 0, sortedEntities.Count - 1);
        sortedEntities.Insert(location, ItemQualityIEntity.iEntity);
        ItemQualityIEntity.transform.SetSiblingIndex(location);
    }
    private int GetLocation(IItem IEntity, int start, int end)
    {
        if (start > end)
            return start;

        int mid = (start + end) / 2;

        IItem IMidEntity = sortedEntities[mid];

        int nameComparison = string.Compare(IMidEntity.GetName(), IEntity.GetName(), StringComparison.OrdinalIgnoreCase);

        if (IMidEntity.GetRaritySO().Rarity < IEntity.GetRaritySO().Rarity)
        {
            return GetLocation(IEntity, start, mid - 1);
        }
        else if (IMidEntity.GetRaritySO().Rarity > IEntity.GetRaritySO().Rarity)
        {
            return GetLocation(IEntity, mid + 1, end);
        }
        else
        {
            if (nameComparison < 0)
            {
                return GetLocation(IEntity, mid + 1, end);
            }
            else if (nameComparison > 0)
            {
                return GetLocation(IEntity, start, mid - 1);
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

            IItem IEntity = sortedEntities[index];

            if (IEntity.GetRaritySO().Rarity > MaxRarity ||
                !slotManager.CanManualAdd(IEntity))
            {
                amount++;
                continue;
            }

            entitiesList.Add(IEntity);
        }

        return entitiesList;
    }

    private void OnItemQualityRemove(ItemQualityButton ItemQualityButton)
    {
        Slot slot = slotManager.Contains(GetIItem(ItemQualityButton));

        if (slot != null)
        {
            slot.DeleteIItem();
            RevealItemCard(null);
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
        if (!itemQualityDictionary.TryGetValue(IItem, out ItemQualityIEntity itemQualityIEntity))
            return null;

        return itemQualityIEntity;
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
        for (int i = itemQualityDictionary.Count - 1; i >= 0; i--)
        {
            Inventory_OnItemRemove(itemQualityDictionary.ElementAt(i).Key);
        }

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
