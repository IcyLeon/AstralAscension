using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class SlotManager : MonoBehaviour
{
    private ItemManagerSO ItemAssetManagerSO;
    private Slot[] slotList;
    private Dictionary<IData, Slot> slotDic;

    public event Action<Slot> OnSlotSelected;
    public event Action<Slot> OnSlotItemAdd;
    public event Action<Slot> OnSlotChanged;
    public event Action<IData, Slot> OnSlotItemRemove;

    public IData iData { get; private set; }

    public void SetIItem(IData IData)
    {
        iData = IData;
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

    private void Start()
    {
        ItemAssetManagerSO = AssetManager.instance.ItemAssetManagerSO;
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

    public bool TryAddEntityToSlot(IData iData)
    {
        Slot slot = GetAvailableSlot();

        if (slot == null)
        {
            PopoutMessageManager.SendPopoutMessage("Slots are fulled!");
            return false;
        }

        ItemQuality itemQuality = ItemAssetManagerSO.CreateItemQualityObject(iData, slot.transform);
        itemQuality.targetGraphic.raycastTarget = false;
        itemQuality.Select();
        slot.Init();

        return true;
    }


    public bool CanManualAdd(IData iData)
    {
        UpgradableItems upgradableItems = iData as UpgradableItems;

        return !Contains(iData) && (upgradableItems == null || (upgradableItems != null && !upgradableItems.locked));
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
        IData iData = Slot.itemQuality.iEntity;

        if (slotDic.TryGetValue(iData, out Slot slot))
            return;

        slotDic.Add(iData, Slot);
        OnSlotItemAdd?.Invoke(Slot);
        OnSlotChanged?.Invoke(Slot);
    }

    private void Slot_OnSlotItemRemove(IData IData, Slot Slot)
    {
        slotDic.Remove(IData);
        OnSlotItemRemove?.Invoke(IData, Slot);
        Slot.transform.SetAsLastSibling();
        InitSlotList();
        OnSlotChanged?.Invoke(Slot);
    }

    public Slot Contains(IData iData)
    {
        if (slotDic.TryGetValue(iData, out Slot slot))
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
            if (slot.itemQuality == null)
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
