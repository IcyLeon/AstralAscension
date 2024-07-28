using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static InventoryManager;

public class SlotPopup : MonoBehaviour
{
    [SerializeField] private ItemManagerSO ItemManagerSO;
    [SerializeField] private ScrollRect ScrollRect;
    [SerializeField] private ItemCard itemCard;
    public event EventHandler<ItemQualityEvents> OnItemQualityClick;

    private SlotManager slotManager;
    private Dictionary<Item, ItemQualityIEntity> itemQualityDictionary;

    private IItem iItemType;
    private Inventory inventory;

    private void Awake()
    {
        OnInventoryOld += InventoryManager_OnInventoryOld;
        OnInventoryNew += InventoryManager_OnInventoryNew;
    }

    private void Init()
    {
        Inventory inventory = instance.inventory;

        if (itemQualityDictionary != null)
        {
            if (inventory != null)
            {
                UpdateVisual();
            }
            return;
        }

        itemQualityDictionary = new();

        if (instance == null)
        {
            Debug.LogError("Inventory Manager not found!");
            return;
        }

        InventoryManager_OnInventoryNew(inventory);
    }    

    private void Start()
    {
        Init();
    }

    private void InventoryManager_OnInventoryOld(Inventory Inventory)
    {
        Inventory.OnItemAdd -= Inventory_OnItemAdd;
        Inventory.OnItemRemove -= Inventory_OnItemRemove;
    }

    private void InventoryManager_OnInventoryNew(Inventory Inventory)
    {
        inventory = Inventory;
        if (inventory != null)
        {
            inventory.OnItemAdd += Inventory_OnItemAdd;
            inventory.OnItemRemove += Inventory_OnItemRemove;
            UpdateVisual();
        }
    }

    private void Inventory_OnItemRemove(Item item)
    {
        if (!itemQualityDictionary.TryGetValue(item, out ItemQualityIEntity itemQualityIEntity))
            return;

        itemQualityIEntity.ItemQualitySelection.OnRemoveClick -= ItemQualitySelection_OnRemoveClick;
        itemQualityIEntity.OnItemQualityClick -= OnSelectedItemQualityClick;
        Destroy(itemQualityIEntity.gameObject);
        itemQualityDictionary.Remove(item);
    }

    public void SetinterfaceItemType(IItem IItem)
    {
        iItemType = IItem;
        Init();
    }

    private bool IsEquippedByCharacter(Item item)
    {
        UpgradableItems upgradableItems = item as UpgradableItems;

        if (upgradableItems == null)
            return false;

        return upgradableItems.equipByCharacter != null;
    }

    private void Inventory_OnItemAdd(Item item)
    {
        if (itemQualityDictionary.ContainsKey(item) || 
            (iItemType != null && (item.GetInterfaceItemReference() != iItemType.GetInterfaceItemReference() || item == iItemType)) ||
            IsEquippedByCharacter(item))
            return;

        GameObject go = Instantiate(ItemManagerSO.ItemQualityItemPrefab, ScrollRect.content);
        ItemQualityIEntity itemQualityIEntity = go.GetComponent<ItemQualityIEntity>();
        itemQualityIEntity.SetIEntity(item);
        itemQualityIEntity.OnItemQualityClick += OnSelectedItemQualityClick;
        itemQualityIEntity.ItemQualitySelection.OnRemoveClick += ItemQualitySelection_OnRemoveClick;
        itemQualityDictionary.Add(item, itemQualityIEntity);
    }

    private void ItemQualitySelection_OnRemoveClick(object sender, System.EventArgs e)
    {
        ItemQualitySelection itemQualitySelection = sender as ItemQualitySelection;

        IEntity iEntity = itemQualitySelection.itemQualityIEntity.iEntity;

        Slot slot = slotManager.Contains(iEntity);

        if (slot != null)
        {
            itemCard.gameObject.SetActive(false);
            slot.SetItemQualityButton(null);
        }    
    }

    public List<Item> GetItemList()
    {
        List<Item> itemList = new();

        if (itemQualityDictionary == null)
            return itemList;

        foreach(var itemKeyPair in itemQualityDictionary)
        {
            itemList.Add(itemKeyPair.Key);
        }

        return itemList;
    }

    public void SetSlotManager(SlotManager SlotManager)
    {
        UnSubscribeEvents();
        slotManager = SlotManager;
        SubscribeEvents();
    }
    private void SubscribeEvents()
    {
        if (slotManager == null)
            return;

        slotManager.OnSlotSelected += SlotManager_OnSlotSelected;
        slotManager.OnSlotItemAdd += SlotManager_OnSlotItemAdd;
        slotManager.OnSlotItemRemove += SlotManager_OnSlotItemRemove;
    }

    private void SlotManager_OnSlotItemAdd(object sender, SlotItemEvent e)
    {
        Item item = e.iEntity as Item;

        if (item == null)
            return;

        if (itemQualityDictionary.ContainsKey(item))
        {
            itemQualityDictionary[item].ItemQualitySelection.gameObject.SetActive(true);
        }
    }

    private void SlotManager_OnSlotItemRemove(object sender, SlotItemEvent e)
    {
        Item item = e.iEntity as Item;

        if (item == null)
            return;

        if (itemQualityDictionary.ContainsKey(item))
        {
            itemQualityDictionary[item].ItemQualitySelection.gameObject.SetActive(false);
        }
    }

    private void UnSubscribeEvents()
    {
        if (slotManager == null)
            return;

        slotManager.OnSlotSelected -= SlotManager_OnSlotSelected;
        slotManager.OnSlotItemAdd -= SlotManager_OnSlotItemAdd;
        slotManager.OnSlotItemRemove -= SlotManager_OnSlotItemRemove;
    }

    private void SlotManager_OnSlotSelected(object sender, SlotEvent e)
    {
        Slot slot = e.slot;

        gameObject.SetActive(slot != null);

        ItemQualityButton itemQualityButton = e.slot.itemQualityButton;

        if (itemQualityButton == null)
            return;

        itemCard.SetInterfaceItem(itemQualityButton.ItemQuality.iItem);
    }

    private void OnSelectedItemQualityClick(object sender, ItemQualityEvents e)
    {
        ItemQualityButton itemQualityButton = sender as ItemQualityButton;
        itemCard.SetInterfaceItem(itemQualityButton.ItemQuality.iItem);
        OnItemQualityClick?.Invoke(sender, e);
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
            Inventory_OnItemAdd(item);
        }
    }

    private void OnDestroy()
    {
        UnSubscribeEvents();
        OnInventoryOld -= InventoryManager_OnInventoryOld;
        OnInventoryNew -= InventoryManager_OnInventoryNew;

        if (inventory != null)
        {
            inventory.OnItemAdd -= Inventory_OnItemAdd;
            inventory.OnItemRemove -= Inventory_OnItemRemove;
        }
    }
}
