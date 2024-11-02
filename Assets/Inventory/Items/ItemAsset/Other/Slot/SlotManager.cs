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

    public bool TryAddEntityToAvailableSlot(IEntity IEntity)
    {
        Slot emptySlot = GetAvailableSlot();

        if (emptySlot == null)
        {
            PopoutMessageManager.SendPopoutMessage(this, "Slots are fulled!");
            return false;
        }

        emptySlot.SetItemQualityButton(IEntity);

        return true;
    }

    public bool CanManualAdd(IItem iItem)
    {
        UpgradableItems upgradableItems = iItem as UpgradableItems;

        return !Contains(iItem) && (upgradableItems == null || (upgradableItems != null && !upgradableItems.locked));
    }

    private void OnDestroy()
    {
        UnSubscribeEvents();
    }

    private void UnSubscribeEvents()
    {
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


    public Slot Contains(IItem iItem)
    {
        foreach (var slot in slotList)
        {
            if (slot.itemQualityButton != null && slot.itemQualityButton.iItem == iItem)
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

            if (ItemQualityIEntity != null)
                ItemEntityList.Add(ItemQualityIEntity.iEntity);
        }

        return ItemEntityList;
    }

    private Slot GetAvailableSlot()
    {
        foreach (var slot in slotList)
        {
            if (slot.itemQualityButton == null)
                return slot;
        }

        return null;
    }

    public int GetTotalSlots()
    {
        return slotList.Length;
    }
}
