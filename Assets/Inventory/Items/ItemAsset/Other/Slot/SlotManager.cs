using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotEvent : EventArgs
{
    public Slot slot;
}

public class SlotManager : MonoBehaviour
{
    [SerializeField] private SlotPopup SlotPopup;
    private Slot[] slotList;
    public event EventHandler<SlotEvent> OnSlotSelected;
    public event EventHandler<SlotItemEvent> OnSlotItemAdd;
    public event EventHandler<SlotItemEvent> OnSlotItemRemove;

    public void SetIItemType(IItem IItem)
    {
        SlotPopup.SetIItemType(IItem);
    }

    public List<IEntity> GetItemList()
    {
        return SlotPopup.GetItemList();
    }

    public void RemoveItems(List<IEntity> ItemEntityList)
    {
        SlotPopup.RemoveItems(ItemEntityList);
    }

    private void Awake()
    {
        slotList = GetComponentsInChildren<Slot>();
        SubscribeEvents();
    }

    private void SlotPopup_OnItemQualityClick(object sender, ItemQualityEvents e)
    {
        IEntity iEntity = e.ItemQualityButton.ItemQuality.iItem as IEntity;

        Slot availableSlot = GetAvailableSlot();

        if (availableSlot != null && CanManualAdd(iEntity))
        {
            availableSlot.SetItemQualityButton(iEntity);
        }
    }

    private bool CanManualAdd(IEntity iEntity)
    {
        UpgradableItems upgradableItems = iEntity as UpgradableItems;

        return !Contains(iEntity) && !upgradableItems.locked;
    }

    public void ResetAllSlots()
    {
        foreach(var slot in slotList)
        {
            slot.SetItemQualityButton(null);
        }
    }

    private void Slot_OnSlotClick(object sender, SlotEvent e)
    {
        OnSlotSelected?.Invoke(this, new SlotEvent { 
            slot = e.slot
        });
    }

    private void OnDestroy()
    {
        UnSubscribeEvents();
    }

    private void UnSubscribeEvents()
    {
        SlotPopup.OnItemQualityClick -= SlotPopup_OnItemQualityClick;

        if (slotList == null)
            return;

        foreach (var slot in slotList)
        {
            slot.OnSlotClick -= Slot_OnSlotClick;
            slot.OnSlotItemAdd -= Slot_OnSlotItemAdd;
            slot.OnSlotItemRemove -= Slot_OnSlotItemRemove;
        }
    }

    private void SubscribeEvents()
    {
        SlotPopup.SetSlotManager(this);

        SlotPopup.OnItemQualityClick += SlotPopup_OnItemQualityClick;

        foreach (var slot in slotList)
        {
            slot.OnSlotClick += Slot_OnSlotClick;
            slot.OnSlotItemAdd += Slot_OnSlotItemAdd;
            slot.OnSlotItemRemove += Slot_OnSlotItemRemove;
        }
    }

    private void Slot_OnSlotItemAdd(object sender, SlotItemEvent e)
    {
        OnSlotItemAdd?.Invoke(sender, e);
    }

    private void Slot_OnSlotItemRemove(object sender, SlotItemEvent e)
    {
        OnSlotItemRemove?.Invoke(sender, e);
    }


    public Slot Contains(IEntity IEntity)
    {
        foreach (var slot in slotList)
        {
            if (slot.itemQualityButton != null && slot.itemQualityButton.ItemQuality.iItem == IEntity)
                return slot;
        }
        return null;
    }

    public List<IEntity> GetItemEntityList()
    {
        List<IEntity> ItemEntityList = new();

        foreach (var slot in slotList)
        {
            ItemQualityIEntity ItemQualityIEntity = slot.itemQualityButton;

            if (ItemQualityIEntity != null && ItemQualityIEntity.iEntity != null)
                ItemEntityList.Add(ItemQualityIEntity.iEntity);
        }

        return ItemEntityList;
    }

    public Slot GetAvailableSlot()
    {
        foreach (var slot in slotList)
        {
            if (slot.itemQualityButton == null)
                return slot;
        }

        return null;
    }
}
