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

    private SlotManager slotManager;
    private Dictionary<IEntity, ItemQualityIEntity> itemQualityDictionary;

    private IItem iItem;
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

        if (InventoryManager.instance == null)
        {
            Debug.LogError("Inventory Manager not found!");
            return;
        }

        InventoryManager_OnInventoryNew(InventoryManager.instance.inventory);
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

    private void Inventory_OnItemRemove(IEntity item)
    {
        if (!itemQualityDictionary.TryGetValue(item, out ItemQualityIEntity itemQualityIEntity))
            return;

        itemQualityIEntity.OnItemQualityClick -= OnSelectedItemQualityButton;
        itemQualityIEntity.OnItemQualityClick -= OnAddItemQualityToSlot;
        itemQualityIEntity.ItemQualitySelection.OnRemoveClick -= OnItemQualityRemove;
        Destroy(itemQualityIEntity.gameObject);
        itemQualityDictionary.Remove(item);
    }

    public void SetIItemType(IItem IItem)
    {
        iItem = IItem;
        Init();
        UpdateVisual();
    }

    private bool IsEquippedByCharacter(IEntity iEntity)
    {
        UpgradableItems upgradableItems = iEntity as UpgradableItems;

        if (upgradableItems == null)
            return false;

        return upgradableItems.equipByCharacter != null;
    }

    private void Inventory_OnItemAdd(IEntity IEntity)
    {
        if (iItem != null &&
            iItem.GetType() == IEntity.GetIItem().GetType() &&
            (itemQualityDictionary.ContainsKey(IEntity) ||
            IEntity == iItem ||
            IsEquippedByCharacter(IEntity)))
            return;

        ItemQualityIEntity itemQualityIEntity = ItemManagerSO.CreateItemQualityItem(IEntity, ScrollRect.content);
        itemQualityIEntity.SetIItem(IEntity);
        itemQualityIEntity.OnItemQualityClick += OnAddItemQualityToSlot;
        itemQualityIEntity.OnItemQualityClick += OnSelectedItemQualityButton;
        itemQualityIEntity.ItemQualitySelection.OnRemoveClick += OnItemQualityRemove;
        itemQualityDictionary.Add(IEntity, itemQualityIEntity);
    }

    private void OnItemQualityRemove(object sender, ItemQualityEvents e)
    {
        Slot slot = slotManager.Contains(e.ItemQualityButton.iItem);

        if (slot != null)
        {
            slot.SetItemQualityButton(null);
            RevealItemCard(null);
        }
    }

    public List<IEntity> GetItemList()
    {
        List<IEntity> itemList = new();

        if (itemQualityDictionary == null)
            return itemList;

        foreach (var itemKeyPair in itemQualityDictionary)
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
        RevealItemCard(e.slot.itemQualityButton);

        ItemQualityIEntity ItemQualityIEntity = GetItemQualityIEntity(e.iEntity);

        if (ItemQualityIEntity == null)
            return;

        ItemQualityIEntity.ItemQualitySelection.RevealSelection();
    }

    private void SlotManager_OnSlotItemRemove(object sender, SlotItemEvent e)
    {
        ItemQualityIEntity ItemQualityIEntity = GetItemQualityIEntity(e.iEntity);

        if (ItemQualityIEntity == null)
            return;

        ItemQualityIEntity.ItemQualitySelection.HideSelection();
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
        RevealItemCard(e.slot.itemQualityButton);
    }

    private ItemQualityIEntity GetItemQualityIEntity(IEntity iEntity)
    {
        if (!itemQualityDictionary.ContainsKey(iEntity))
            return null;

        return itemQualityDictionary[iEntity];
    }

    private void RevealItemCard(ItemQualityButton ItemQualityButton)
    {
        gameObject.SetActive(true);

        if (ItemQualityButton == null)
        {
            itemCard.SetIItem(null);
            return;
        }

        itemCard.SetIItem(ItemQualityButton.iItem);
    }

    private void OnSelectedItemQualityButton(object sender, ItemQualityEvents e)
    {
        RevealItemCard(e.ItemQualityButton);
    }

    private void OnAddItemQualityToSlot(object sender, ItemQualityEvents e)
    {
        IEntity iEntity = e.ItemQualityButton.iItem as IEntity;

        if (!slotManager.CanManualAdd(iEntity.GetIItem()))
            return;

        slotManager.TryAddEntityToAvailableSlot(iEntity);
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
