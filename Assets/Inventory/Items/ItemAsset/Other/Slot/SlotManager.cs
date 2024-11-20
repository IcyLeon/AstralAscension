using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class SlotManager : MonoBehaviour
{
    private Slot[] slotList;
    private Dictionary<IItem, Slot> slotDic;

    public event Action<Slot> OnSlotSelected;
    public event Action<Slot> OnSlotItemAdd;
    public event Action<Slot> OnSlotChanged;
    public event Action<IItem, Slot> OnSlotItemRemove;

    public IItem iItem { get; private set; }

    public void SetIItem(IItem IItem)
    {
        iItem = IItem;
        ResetAllSlots();
    }

    private void Awake()
    {
        InitSlotList();
        slotDic = new();

        foreach (var slot in slotList)
        {
            slot.OnSlotClick += Slot_OnSlotClick;
            slot.OnSlotItemAdd += Slot_OnSlotItemAdd;
            slot.OnSlotItemRemove += Slot_OnSlotItemRemove;
        }
    }

    private void InitSlotList()
    {
        slotList = GetComponentsInChildren<Slot>();
    }

    public void ResetAllSlots()
    {
        foreach(var slot in slotList)
        {
            slot.DeleteIItem();
        }

        slotDic.Clear();
    }

    private void Slot_OnSlotClick(Slot slot)
    {
        OnSlotSelected?.Invoke(slot);
    }

    public bool TryAddEntityToSlot(IItem IItem)
    {
        Slot emptySlot = GetAvailableSlot();

        if (emptySlot == null)
        {
            PopoutMessageManager.SendPopoutMessage("Slots are fulled!");
            return false;
        }

        emptySlot.AddIItem(IItem);
        return true;
    }


    public bool CanManualAdd(IItem iItem)
    {
        UpgradableItems upgradableItems = iItem as UpgradableItems;

        return !Contains(iItem) && (upgradableItems == null || (upgradableItems != null && !upgradableItems.locked));
    }

    private void OnDestroy()
    {
        foreach (var slot in slotList)
        {
            slot.OnSlotClick -= Slot_OnSlotClick;
            slot.OnSlotItemAdd -= Slot_OnSlotItemAdd;
            slot.OnSlotItemRemove -= Slot_OnSlotItemRemove;
        }
    }

    private void Slot_OnSlotItemAdd(Slot Slot)
    {
        IItem iItem = Slot.itemQualityButton.iEntity;

        if (slotDic.TryGetValue(iItem, out Slot slot))
            return;

        slotDic.Add(iItem, Slot);
        OnSlotItemAdd?.Invoke(Slot);
        OnSlotChanged?.Invoke(Slot);
    }

    private void Slot_OnSlotItemRemove(IItem IItem, Slot Slot)
    {
        slotDic.Remove(IItem);
        OnSlotItemRemove?.Invoke(IItem, Slot);
        Slot.transform.SetAsLastSibling();
        InitSlotList();
        OnSlotChanged?.Invoke(Slot);
    }

    public Slot Contains(IItem IItem)
    {
        if (slotDic.TryGetValue(IItem, out Slot slot))
            return slot;

        return null;
    }

    public List<IEntity> GetAllSlotEntities()
    {
        List<IEntity> ItemEntityList = new();

        foreach (var slot in slotDic)
        {
            ItemEntityList.Add(slot.Key as IEntity);
        }

        return ItemEntityList;
    }

    private Slot GetAvailableSlot()
    {
        foreach(var slot in slotList)
        {
            if (slot.itemQualityButton == null)
                return slot;
        }

        return null;
    }

    public int GetTotalAvailableSlots()
    {
        return slotList.Length - slotDic.Count;
    }

    public int GetTotalUsedUpSlots()
    {
        return slotDic.Count;
    }
}
