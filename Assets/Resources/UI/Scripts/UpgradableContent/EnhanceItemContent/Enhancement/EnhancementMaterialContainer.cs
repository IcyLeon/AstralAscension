using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[DisallowMultipleComponent]
public class EnhancementMaterialContainer : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private Button AutoAddBtn;
    [SerializeField] private Button EnhanceBtn;
    [SerializeField] private SlotPopup SlotPopup;
    private SlotManager slotManager;
    private Rarity raritySelection;
    private EnhancementManager enhancementManager;
    private UpgradableItems upgradableItem;
    private int TotalExpAmount;
    public event Action<int> OnUpgradeClick;
    public event Action<int> OnSlotChanged;

    private void Awake()
    {
        TotalExpAmount = 0;

        enhancementManager = GetComponentInParent<EnhancementManager>();
        enhancementManager.OnEnhanceItemChanged += EnhancePanel_OnEnhanceItemChanged;

        slotManager = GetComponentInChildren<SlotManager>();
        SlotPopup.SetSlotManager(slotManager);

        SlotPopup.OnSlotChanged += SlotManager_OnSlotChanged;

        AutoAddBtn.onClick.AddListener(OnAutoAdd);
        EnhanceBtn.onClick.AddListener(OnEnhance);

        dropdown.onValueChanged.AddListener(delegate
        {
            OnDropdown(dropdown);
        });
    }

    private void Start()
    {
        UpdateVisual();
    }

    private void SlotManager_OnSlotChanged(Slot Slot)
    {
        TotalExpAmount = GetIncreaseTotalExp();
        OnSlotChanged?.Invoke(TotalExpAmount);
    }

    private int GetIncreaseTotalExp()
    {
        int total = 0;

        if (upgradableItem == null)
        {
            return total;
        }

        List<IEntity> entitiesList = SlotPopup.GetAllSlotEntities();

        for (int i = 0; i < entitiesList.Count; i++)
        {
            UpgradableItems upgradableItem = entitiesList[i] as UpgradableItems;
            int baseExp = upgradableItem.expCostManagerSO.GetBaseEnhancementEXP(upgradableItem.GetRaritySO());
            float enhancedEXP = 0.8f * GetTotalEXP(upgradableItem, upgradableItem.level);
            total += baseExp + Mathf.RoundToInt(enhancedEXP);
        }

        int totalEnhancementRequired = GetTotalEXP(upgradableItem, upgradableItem.maxLevel) - upgradableItem.currentEXP;
        int totalEnhancementEXP = GetTotalEXP(upgradableItem, upgradableItem.level) + total;
        if (totalEnhancementEXP >= totalEnhancementRequired)
        {
            int diff = totalEnhancementEXP - totalEnhancementRequired;
            total -= diff;
        }

        return total;
    }

    private int GetTotalEXP(UpgradableItems UpgradableItems, int level)
    {
        if (UpgradableItems == null)
            return 0;

        return UpgradableItems.currentEXP + UpgradableItems.expCostManagerSO.GetTotalExpRequired(level, UpgradableItems.GetRaritySO());
    }


    private void OnDropdown(TMP_Dropdown dropdown)
    {
        raritySelection = (Rarity)dropdown.value;
    }

    private void EnhancePanel_OnEnhanceItemChanged()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        upgradableItem = enhancementManager.upgradableItem;

        if (upgradableItem == null)
            return;

        SlotPopup.SetIItem(upgradableItem);
    }

    private void OnAutoAdd()
    {
        List<IItem> allrelatedItems = SlotPopup.GetEntityList(raritySelection, slotManager.GetTotalAvailableSlots());

        for (int i = 0; i < allrelatedItems.Count; i++)
        {
            IItem IEntity = allrelatedItems[i];

            if (!SlotPopup.AddEntityToSlot(IEntity))
            {
                return;
            }
        }

        if (allrelatedItems.Count == 0)
        {
            PopoutMessageManager.SendPopoutMessage("No Consumables Found!");
            return;
        }
    }

    private void OnEnhance()
    {
        if (upgradableItem == null || slotManager.GetTotalUsedUpSlots() == 0)
        {
            return;
        }

        OnUpgradeClick?.Invoke(TotalExpAmount);

        SlotPopup.RemoveItems(SlotPopup.GetAllSlotEntities());

        slotManager.ResetAllSlots();
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        slotManager.OnSlotChanged -= SlotManager_OnSlotChanged;

        if (enhancementManager != null)
        {
            enhancementManager.OnEnhanceItemChanged -= EnhancePanel_OnEnhanceItemChanged;
        }

        AutoAddBtn.onClick.RemoveAllListeners();
        EnhanceBtn.onClick.RemoveAllListeners();

    }
}
