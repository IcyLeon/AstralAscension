using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static InventoryManager;
using Random = UnityEngine.Random;

public class UpgradeEvents : EventArgs
{
    public int Exp;
}

[DisallowMultipleComponent]
public class EnhancementMaterialContainer : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private Button AutoAddBtn;
    [SerializeField] private Button EnhanceBtn;
    private Rarity raritySelection;
    private EnhancementManager enhancementManager;
    private SlotManager slotManager;
    public event EventHandler<UpgradeEvents> OnUpgradeClick;
    public event EventHandler<UpgradeEvents> OnSlotItemChanged;
    private IEXP iEXPEntity;
    private int AddExpAmount;

    private void Awake()
    {
        AddExpAmount = 0;

        OnInventoryNew += InventoryManager_OnInventoryNew;

        enhancementManager = GetComponentInParent<EnhancementManager>();
        enhancementManager.OnEnhanceItemChanged += EnhancePanel_OnEnhanceItemChanged;

        slotManager = GetComponentInChildren<SlotManager>();

        slotManager.OnSlotItemAdd += SlotManager_OnSlotItemAdd;
        slotManager.OnSlotItemRemove += SlotManager_OnSlotItemRemove;

        AutoAddBtn.onClick.AddListener(OnAutoAdd);
        EnhanceBtn.onClick.AddListener(OnEnhance);

        dropdown.onValueChanged.AddListener(delegate
        {
            OnDropdown(dropdown);
        });
    }

    private void SlotManager_OnSlotItemRemove(object sender, SlotItemEvent e)
    {
        CallOnSlotItemChanged(this);
    }

    private void SlotManager_OnSlotItemAdd(object sender, SlotItemEvent e)
    {
        CallOnSlotItemChanged(this);
    }

    private void CallOnSlotItemChanged(object sender)
    {
        List<IEntity> itemEntityList = slotManager.GetItemEntityList();

        AddExpAmount = GetTotalExp(itemEntityList);

        OnSlotItemChanged?.Invoke(sender, new UpgradeEvents
        {
            Exp = AddExpAmount

        });
    }

    private int GetTotalExp(List<IEntity> entitiesList)
    {
        int total = 0;

        for(int i = 0; i < entitiesList.Count; i++)
        {
            IEXP iexpEntity = entitiesList[i] as IEXP;
            if (iexpEntity == null)
            {
                continue;
            }

            int baseExp = iexpEntity.GetExpCostSO().GetBaseEnhancementEXP(entitiesList[i].GetRarity());
            float enhancedEXP = 0.8f * (iexpEntity.GetCurrentExp() + iexpEntity.GetExpCostSO().GetTotalExpRequired(iexpEntity.GetLevel(), entitiesList[i].GetRarity()));


            total += baseExp + Mathf.RoundToInt(enhancedEXP);

        }

        int totalEnhancementRequired = GetTotalEnhancementEXP();
        int totalCurrentEXP = GetCurrentTotalEnhancementEXP();
        int totalEnhancementEXP = totalCurrentEXP + total;

        if (totalEnhancementEXP >= totalEnhancementRequired)
        {
            int diff = totalEnhancementEXP - totalEnhancementRequired;
            total -= diff;
        }

        return total;
    }

    private int GetTotalEnhancementEXP()
    {
        if (iEXPEntity == null)
        {
            return 0;
        }

        return iEXPEntity.GetExpCostSO().GetTotalExpRequired(iEXPEntity.GetMaxLevel(), iEXPEntity.GetIEntity().GetRarity());
    }

    private int GetCurrentTotalEnhancementEXP()
    {
        if (iEXPEntity == null)
        {
            return 0;
        }

        return iEXPEntity.GetCurrentExp() + iEXPEntity.GetExpCostSO().GetTotalExpRequired(iEXPEntity.GetLevel(), iEXPEntity.GetIEntity().GetRarity());
    }


    private void Start()
    {
        Init();
    }

    private void OnDropdown(TMP_Dropdown dropdown)
    {
        raritySelection = (Rarity)dropdown.value;
    }

    private void EnhancePanel_OnEnhanceItemChanged(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        iEXPEntity = enhancementManager.iEXPEntity;
        ResetSlots();
        slotManager.SetIItemType(iEXPEntity.GetIEntity());
    }

    private void Init()
    {
        if (instance == null)
        {
            Debug.LogError("Inventory Manager  not found!");
            return;
        }

        UpdateVisual();

        InventoryManager_OnInventoryNew(instance.inventory);
    }

    private void ResetSlots()
    {
        slotManager.ResetAllSlots();
    }

    private void InventoryManager_OnInventoryNew(Inventory Inventory)
    {
        ResetSlots();
    }

    private void QuickSort(List<IEntity> list, int left, int right)
    {
        if (left < right)
        {
            int pivot = Partition(list, left, right);

            QuickSort(list, left, pivot - 1);
            QuickSort(list, pivot + 1, right);
        }
    }

    private int Partition(List<IEntity> list, int left, int right)
    {
        Rarity pivot = list[right].GetRarity();
        int i = left - 1;

        for (int j = left; j < right; j++)
        {
            if (list[j].GetRarity() <= pivot)
            {
                i++;
                Swap(ref list, i, j);
            }
        }

        Swap(ref list, i + 1, right);

        return i + 1;
    }

    private void Swap(ref List<IEntity> list, int first, int second)
    {
        var temp = list[first];
        list[first] = list[second];
        list[second] = temp;
    }

    private List<IEntity> GetRelatedItemList(List<IEntity> list)
    {
        List<IEntity> IEntityList = new();

        foreach(var item in list)
        {
            if (item != null &&
                item.GetRarity() <= raritySelection
                && slotManager.CanManualAdd(item))
            {
                IEntityList.Add(item);
            }
        }

        return IEntityList;
    }

    private void OnAutoAdd()
    {
        List<IEntity> allrelatedItems = GetRelatedItemList(slotManager.GetItemList());

        if (allrelatedItems.Count == 0)
        {
            PopoutMessageManager.SendPopoutMessage(this, "No Consumables Found!");
            return;
        }

        for (int i = 0; i < allrelatedItems.Count; i++)
        {
            int randomValue = i + Random.Range(0, allrelatedItems.Count - i);
            Swap(ref allrelatedItems, i, randomValue);
        }

        QuickSort(allrelatedItems, 0, allrelatedItems.Count - 1);

        for (int i = 0; i < slotManager.GetTotalSlots(); i++)
        {
            if (i < allrelatedItems.Count && !slotManager.TryAddEntityToAvailableSlot(allrelatedItems[i]))
            {
                return;
            }
        }
    }

    private void OnEnhance()
    {
        List<IEntity> itemEntityList = slotManager.GetItemEntityList();

        if (itemEntityList.Count == 0)
        {
            return;
        }

        OnUpgradeClick?.Invoke(this, new UpgradeEvents
        {
            Exp = AddExpAmount
        });

        ResetSlots();

        slotManager.RemoveItems(itemEntityList);
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        OnInventoryNew -= InventoryManager_OnInventoryNew;

        slotManager.OnSlotItemAdd -= SlotManager_OnSlotItemAdd;
        slotManager.OnSlotItemRemove -= SlotManager_OnSlotItemRemove;

        if (enhancementManager != null)
        {
            enhancementManager.OnEnhanceItemChanged -= EnhancePanel_OnEnhanceItemChanged;
        }
    }
}
