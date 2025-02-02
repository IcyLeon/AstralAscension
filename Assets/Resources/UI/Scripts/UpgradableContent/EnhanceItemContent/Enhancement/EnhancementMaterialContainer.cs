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
    private Rarity raritySelection;
    private EnhancementManager enhancementManager;
    public event Action<int> OnUpgradeClick;
    public event Action<int> OnSlotItemChanged;
    private IEXP iEXPEntity;
    private int AddExpAmount;

    private void Awake()
    {
        AddExpAmount = 0;

        enhancementManager = GetComponentInParent<EnhancementManager>();
        enhancementManager.OnEnhanceItemChanged += EnhancePanel_OnEnhanceItemChanged;

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
        AddExpAmount = GetAddTotalExp();
        OnSlotItemChanged?.Invoke(AddExpAmount);
    }

    private int GetAddTotalExp()
    {
        List<IEntity> entitiesList = SlotPopup.GetAllSlotEntities();
        int total = 0;

        for (int i = 0; i < entitiesList.Count; i++)
        {
            IEXP iExpEntity = entitiesList[i] as IEXP;

            if (iExpEntity == null)
            {
                return 0;
            }

            int baseExp = iExpEntity.GetExpCostSO().GetBaseEnhancementEXP(iExpEntity.GetIEntity().GetRaritySO());
            float enhancedEXP = 0.8f * GetTotalEXP(iExpEntity);
            total += baseExp + Mathf.RoundToInt(enhancedEXP);
        }

        int totalEnhancementRequired = GetTotalEnhancementEXP();
        int totalEnhancementEXP = GetTotalEXP(iEXPEntity) + total;
        if (totalEnhancementEXP >= totalEnhancementRequired)
        {
            int diff = totalEnhancementEXP - totalEnhancementRequired;
            total -= diff;
        }

        return total;
    }

    private int GetTotalEXP(IEXP IExpEntity)
    {
        if (IExpEntity == null)
            return 0;

        return IExpEntity.GetCurrentExp() + IExpEntity.GetExpCostSO().GetTotalExpRequired(IExpEntity.GetLevel(), IExpEntity.GetIEntity().GetRaritySO());
    }

    private int GetTotalEnhancementEXP()
    {
        if (iEXPEntity == null)
        {
            return 0;
        }

        return iEXPEntity.GetExpCostSO().GetTotalExpRequired(iEXPEntity.GetMaxLevel(), iEXPEntity.GetIEntity().GetRaritySO());
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
        iEXPEntity = enhancementManager.iEXPEntity;

        if (iEXPEntity == null)
            return;

        SlotPopup.SetIItem(iEXPEntity.GetIEntity());
    }

    private void OnAutoAdd()
    {
        List<IItem> allrelatedItems = SlotPopup.GetEntityList(raritySelection, SlotPopup.slotManager.GetTotalAvailableSlots());

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
        if (SlotPopup.slotManager.GetTotalUsedUpSlots() == 0)
        {
            return;
        }

        OnUpgradeClick?.Invoke(AddExpAmount);

        SlotPopup.RemoveItems(SlotPopup.GetAllSlotEntities());

        SlotPopup.slotManager.ResetAllSlots();
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        SlotPopup.slotManager.OnSlotChanged -= SlotManager_OnSlotChanged;

        if (enhancementManager != null)
        {
            enhancementManager.OnEnhanceItemChanged -= EnhancePanel_OnEnhanceItemChanged;
        }

        AutoAddBtn.onClick.RemoveAllListeners();
        EnhanceBtn.onClick.RemoveAllListeners();

    }
}
