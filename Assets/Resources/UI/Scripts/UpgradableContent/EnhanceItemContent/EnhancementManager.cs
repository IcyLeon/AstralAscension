using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class EnhancementManager : MonoBehaviour
{
    [SerializeField] private GameObject ButtonMask;

    private EnhancementMaterialContainer EnhancementMaterialContainer;
    private EnhanceStatsPanel EnhanceStatsPanel;
    public IEXP iEXPEntity { get; private set; }
    private EnhancePanel enhancePanel;
    private Coroutine enhancingCoroutine;

    public event Action OnEnhanceItemChanged;
    public event Action OnItemUpgrade;
    public event Action<int> OnSlotItemChanged;
    public int requiredEXP { get; private set; }

    private void Awake()
    {
        requiredEXP = 0;
        EnhanceStatsPanel = GetComponentInChildren<EnhanceStatsPanel>();

        EnhancementMaterialContainer = GetComponentInChildren<EnhancementMaterialContainer>();
        EnhancementMaterialContainer.OnUpgradeClick += EnhancementMaterialContainer_OnUpgradeClick;

        enhancePanel = GetComponentInParent<EnhancePanel>(true);
        if (enhancePanel != null)
        {
            enhancePanel.OnUpgradableItemChanged += EnhancePanel_OnUpgradableItemChanged;
            UpdateVisual();
        }

        EnhancementMaterialContainer.OnSlotItemChanged += EnhancementMaterialContainer_OnSlotItemChanged;
    }

    public int GetIncreasedPreviewLevel(int addExp)
    {
        int levelIncrease = 0;
        int totalExp = iEXPEntity.GetCurrentExp() + addExp;

        ItemRaritySO itemRaritySO = iEXPEntity.GetIEntity().GetRaritySO();

        for (int i = iEXPEntity.GetLevel(); i < iEXPEntity.GetExpCostSO().GetMaxLevel(itemRaritySO); i++)
        {
            int requiredAmt = iEXPEntity.GetExpCostSO().GetRequiredEXP(i, itemRaritySO);
            if (totalExp >= requiredAmt)
            {
                totalExp -= requiredAmt;
                levelIncrease++;
            }
        }

        return levelIncrease;
    }


    private bool IsUpgrading()
    {
        return enhancingCoroutine != null;
    }

    private void EnhancementMaterialContainer_OnSlotItemChanged(int Exp)
    {
        OnSlotItemChanged?.Invoke(Exp);
    }

    private void EnhancementMaterialContainer_OnUpgradeClick(int Exp)
    {
        if (IsUpgrading())
            return;

        enhancingCoroutine = StartCoroutine(UpgradingEnumerator(Exp));
    }

    private IEnumerator UpgradingEnumerator(int addExpAmount)
    {
        float elapseTime = 0f;
        float duration = 1.25f;

        ButtonMask.SetActive(true);

        if (iEXPEntity == null)
        {
            enhancingCoroutine = null;
            yield break;
        }

        iEXPEntity.AddExp(addExpAmount);

        do
        {
            UpdateEnhancingItem();

            EnhanceStatsPanel.SetCurrentEXP(Mathf.Lerp(EnhanceStatsPanel.GetCurrentEXP(), iEXPEntity.GetCurrentExp(),
                elapseTime / (duration * 2f)));

            elapseTime += Time.unscaledDeltaTime;

            yield return null;

        } while (elapseTime <= duration);

        UpdateEnhancingItem();

        OnItemUpgrade?.Invoke();
        ButtonMask.SetActive(false);
        enhancingCoroutine = null;
    }

    private void UpdateEnhancingItem()
    {
        if (GetDisplayExpSliderValue() < requiredEXP)
            return;

        iEXPEntity.RemoveExp(requiredEXP);
        iEXPEntity.Upgrade();
    }


    private int GetDisplayExpSliderValue()
    {
        return Mathf.RoundToInt(EnhanceStatsPanel.GetCurrentEXP());
    }

    private void EnhancePanel_OnUpgradableItemChanged()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        UnsubscribeEvents();
        iEXPEntity = enhancePanel.iEXPEntity;
        SubscribeEvents();
        OnEnhanceItemChanged?.Invoke();
    }

    private void UnsubscribeEvents()
    {
        if (iEXPEntity == null)
            return;

        iEXPEntity.OnUpgradeIEXP -= IEXPEntity_OnUpgradeIEXP;
    }

    private void SubscribeEvents()
    {
        if (iEXPEntity == null)
            return;

        iEXPEntity.OnUpgradeIEXP += IEXPEntity_OnUpgradeIEXP;
        UpdateEXPRequirement();
    }

    private void IEXPEntity_OnUpgradeIEXP()
    {
        UpdateEXPRequirement();
    }

    private void UpdateEXPRequirement()
    {
        requiredEXP = iEXPEntity.GetExpCostSO().GetRequiredEXP(iEXPEntity.GetLevel(), iEXPEntity.GetIEntity().GetRaritySO());
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
        EnhancementMaterialContainer.OnUpgradeClick -= EnhancementMaterialContainer_OnUpgradeClick;
        EnhancementMaterialContainer.OnSlotItemChanged -= EnhancementMaterialContainer_OnSlotItemChanged;
        if (enhancePanel != null)
        {
            enhancePanel.OnUpgradableItemChanged -= EnhancePanel_OnUpgradableItemChanged;
        }
    }
}
