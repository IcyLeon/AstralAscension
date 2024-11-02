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

    [Header("Enhancement Reference")]
    [SerializeField] private EnhancementMaterialContainer EnhancementMaterialContainer;
    [SerializeField] private EnhanceStatsPanel EnhanceStatsPanel;
    public IEXP iEXPEntity { get; private set; }
    private EnhancePanel enhancePanel;
    private Coroutine enhancingCoroutine;

    public event EventHandler OnEnhanceItemChanged;
    public event EventHandler OnItemUpgrade;
    public event EventHandler<UpgradeEvents> OnSlotItemChanged;

    private void Awake()
    {
        EnhancementMaterialContainer.OnUpgradeClick += EnhancementMaterialContainer_OnUpgradeClick;

        enhancePanel = GetComponentInParent<EnhancePanel>(true);
        if (enhancePanel != null)
        {
            enhancePanel.OnUpgradableItemChanged += EnhancePanel_OnUpgradableItemChanged;
            UpdateVisual();
        }

        EnhancementMaterialContainer.OnSlotItemChanged += EnhancementMaterialContainer_OnSlotItemChanged;
    }

    private bool IsUpgrading()
    {
        return enhancingCoroutine != null;
    }

    private void EnhancementMaterialContainer_OnSlotItemChanged(object sender, UpgradeEvents e)
    {
        OnSlotItemChanged?.Invoke(sender, e);
    }

    private void EnhancementMaterialContainer_OnUpgradeClick(object sender, UpgradeEvents e)
    {
        if (IsUpgrading())
            return;

        enhancingCoroutine = StartCoroutine(UpgradingEnumerator(e.Exp));
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

        iEXPEntity.SetCurrentExp(iEXPEntity.GetCurrentExp() + addExpAmount);

        do
        {
            UpdateEnhancingItem(iEXPEntity);

            EnhanceStatsPanel.SetExpSliderValue(Mathf.Lerp(EnhanceStatsPanel.GetExpSliderValue(), iEXPEntity.GetCurrentExp(),
                elapseTime / (duration * 2f)));

            elapseTime += Time.unscaledDeltaTime;

            yield return null;

        } while (elapseTime <= duration);

        UpdateEnhancingItem(iEXPEntity);

        OnItemUpgrade?.Invoke(this, EventArgs.Empty);
        ButtonMask.SetActive(false);
        enhancingCoroutine = null;
    }

    private void UpdateEnhancingItem(IEXP iEXPEntity)
    {
        int requiredEXP = iEXPEntity.GetExpCostSO().GetRequiredEXP(iEXPEntity.GetLevel(), iEXPEntity.GetIEntity().GetRarity());

        if (GetDisplayExpSliderValue() < requiredEXP)
            return;

        iEXPEntity.SetCurrentExp(iEXPEntity.GetCurrentExp() - requiredEXP);
        iEXPEntity.Upgrade();
    }


    private int GetDisplayExpSliderValue()
    {
        return Mathf.RoundToInt(EnhanceStatsPanel.GetExpSliderValue());
    }

    private void EnhancePanel_OnUpgradableItemChanged(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        iEXPEntity = enhancePanel.iEXPEntity;
        OnEnhanceItemChanged?.Invoke(this, EventArgs.Empty);
    }

    private void OnDestroy()
    {
        EnhancementMaterialContainer.OnUpgradeClick -= EnhancementMaterialContainer_OnUpgradeClick;
        EnhancementMaterialContainer.OnSlotItemChanged -= EnhancementMaterialContainer_OnSlotItemChanged;
        if (enhancePanel != null)
        {
            enhancePanel.OnUpgradableItemChanged -= EnhancePanel_OnUpgradableItemChanged;
        }
    }
}
