using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SlotPopup : MonoBehaviour
{
    [SerializeField] private ItemManagerSO ItemManagerSO;
    [SerializeField] private ScrollRect ScrollRect;
    [SerializeField] private ItemCard itemCard;
    public event EventHandler<ItemQualityEvents> OnItemQualityClick;

    private SlotManager slotManager;
    private Dictionary<IEntity, ItemQualityIEntity> itemQualityDictionary;

    private IItem iItemType;
    private Inventory inventory;

    private void Awake()
    {
        InventoryManager.OnInventoryOld += InventoryManager_OnInventoryOld;
        InventoryManager.OnInventoryNew += InventoryManager_OnInventoryNew;
    }

    private void Init()
    {
        if (itemQualityDictionary != null)
            return;

        itemQualityDictionary = new();

        InventoryManager IM = InventoryManager.instance;

        if (IM == null)
        {
            Debug.LogError("Inventory Manager not found!");
            return;
        }

        InventoryManager_OnInventoryNew(IM.inventory);
    }    

    private void Start()
    {
        Init();
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
        inventory = Inventory;
        if (inventory != null)
        {
            inventory.OnItemAdd += Inventory_OnItemAdd;
            inventory.OnItemRemove += Inventory_OnItemRemove;
            UpdateVisual();
        }
    }

    private void Inventory_OnItemRemove(IEntity item)
    {
        if (!itemQualityDictionary.TryGetValue(item, out ItemQualityIEntity itemQualityIEntity))
            return;

        itemQualityIEntity.ItemQualitySelection.OnRemoveClick -= ItemQualitySelection_OnRemoveClick;
        itemQualityIEntity.OnItemQualityClick -= OnSelectedItemQualityClick;
        Destroy(itemQualityIEntity.gameObject);
        itemQualityDictionary.Remove(item);
    }

    public void SetIItemType(IItem IItem)
    {
        iItemType = IItem;
        Init();
        UpdateVisual();
    }

    private bool IsEquippedByCharacter(IEntity item)
    {
        UpgradableItems upgradableItems = item as UpgradableItems;

        if (upgradableItems == null)
            return false;

        return upgradableItems.equipByCharacter != null;
    }

    private void Inventory_OnItemAdd(IEntity item)
    {
        if (iItemType != null &&
            iItemType.GetInterfaceItemReference().GetType() == item.GetInterfaceItemReference().GetType() &&
            (itemQualityDictionary.ContainsKey(item) || 
            item == iItemType ||
            IsEquippedByCharacter(item)))
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
            HideItemCard();
            slot.SetItemQualityButton(null);
        }    
    }

    public List<IEntity> GetItemList()
    {
        List<IEntity> itemList = new();

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
            SelectedItemQuality(itemQualityDictionary[item]);
            RevealItemCard(itemQualityDictionary[item]);
        }
    }

    private void SelectedItemQuality(ItemQualityIEntity ItemQualityIEntity)
    {
        ItemQualityIEntity.ItemQualitySelection.RevealSelection();

        ItemQualityItem itemQualityItem = ItemQualityIEntity as ItemQualityItem;

        if (itemQualityItem == null)
            return;

        itemQualityItem.HideNewStatus();
    }

    private void SlotManager_OnSlotItemRemove(object sender, SlotItemEvent e)
    {
        Item item = e.iEntity as Item;

        if (item == null)
            return;

        if (itemQualityDictionary.ContainsKey(item))
        {
            itemQualityDictionary[item].ItemQualitySelection.HideSelection();
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
        RevealItemCard(slot.itemQualityButton);
        gameObject.SetActive(slot != null);
    }

    private void RevealItemCard(ItemQualityButton ItemQualityButton)
    {
        gameObject.SetActive(ItemQualityButton != null);

        if (ItemQualityButton == null)
        {
            HideItemCard();
            return;
        }

        itemCard.SetInterfaceItem(ItemQualityButton.ItemQuality.iItem);
    }
    private void HideItemCard()
    {
        itemCard.SetInterfaceItem(null);
    }

    private void OnSelectedItemQualityClick(object sender, ItemQualityEvents e)
    {
        RevealItemCard(e.ItemQualityButton);
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
        InventoryManager.OnInventoryOld -= InventoryManager_OnInventoryOld;
        InventoryManager.OnInventoryNew -= InventoryManager_OnInventoryNew;

        if (inventory != null)
        {
            inventory.OnItemAdd -= Inventory_OnItemAdd;
            inventory.OnItemRemove -= Inventory_OnItemRemove;
        }
    }
}
