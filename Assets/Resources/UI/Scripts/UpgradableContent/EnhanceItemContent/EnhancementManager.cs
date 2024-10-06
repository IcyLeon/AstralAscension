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
    [SerializeField] private Slider ExpSlider;

    [Header("Enhancement Reference")]
    [SerializeField] private EnhancementMaterialContainer EnhancementMaterialContainer;
    [SerializeField] private EnhanceStatsPanel EnhanceStatsPanel;
    public IItem iItem { get; private set; }
    private EnhancePanel enhancePanel;
    private Coroutine enhancingCoroutine;

    public event EventHandler OnEnhanceItemChanged;
    public event EventHandler OnItemUpgrade;

    private void Awake()
    {
        EnhancementMaterialContainer.OnUpgradeClick += EnhancementMaterialContainer_OnUpgradeClick;

        enhancePanel = GetComponentInParent<EnhancePanel>(true);
        if (enhancePanel != null)
        {
            enhancePanel.OnUpgradableItemChanged += EnhancePanel_OnUpgradableItemChanged;
            UpdateVisual();
        }
    }

    private void EnhancementMaterialContainer_OnUpgradeClick(object sender, UpgradeEvents e)
    {
        if (enhancingCoroutine != null)
            return;

        enhancingCoroutine = StartCoroutine(UpgradingEnumerator(1000));
    }

    private void AddExpToItem(int amount)
    {
        IEXP iEXPEntity = iItem as IEXP;

        if (iEXPEntity == null)
            return;

        iEXPEntity.AddCurrentExp(amount);
    }

    private IEnumerator UpgradingEnumerator(int addExpAmount)
    {
        float elapseTime = 0f;
        float duration = 1.25f;

        ButtonMask.SetActive(true);

        IEXP iEXPEntity = iItem as IEXP;

        if (iEXPEntity == null)
        {
            enhancingCoroutine = null;
            yield break;
        }

        AddExpToItem(addExpAmount);

        do
        {
            int requiredEXP = iEXPEntity.GetExpCostSO().GetRequiredEXP(iEXPEntity.GetLevel(), iItem.GetRarity());

            if (GetExpSliderValue() >= requiredEXP)
            {
                AddExpToItem(-requiredEXP);
                UpgradeItem();
            }

            ExpSlider.value = Mathf.Lerp(ExpSlider.value, iEXPEntity.GetCurrentExp(),
                elapseTime / (duration * 2f));

            elapseTime += Time.unscaledDeltaTime;

            yield return null;

        } while (elapseTime <= duration);

        OnItemUpgrade?.Invoke(this, EventArgs.Empty);
        ButtonMask.SetActive(false);
        enhancingCoroutine = null;
    }

    private void ResetExpSlider()
    {
        ExpSlider.value = 0f;
    }

    private void InitExpSliderValue()
    {
        IEXP iEXPEntity = iItem as IEXP;

        if (iEXPEntity == null)
            return;

        ExpSlider.value = iEXPEntity.GetCurrentExp();
    }

    private int GetExpSliderValue()
    {
        return Mathf.RoundToInt(ExpSlider.value);
    }

    private void UpgradeItem()
    {
        ResetExpSlider();
        UpdateMaxSliderValue();

        UpgradableItems upgradableItems = iItem as UpgradableItems;

        if (upgradableItems == null)
            return;

        upgradableItems.Upgrade();
    }

    private void EnhancePanel_OnUpgradableItemChanged(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateMaxSliderValue()
    {
        IEXP iEXPEntity = iItem as IEXP;

        if (iEXPEntity == null || iEXPEntity.GetExpCostSO() == null)
            return;

        ExpSlider.maxValue = iEXPEntity.GetExpCostSO().GetRequiredEXP(iEXPEntity.GetLevel(), iItem.GetRarity());
    }

    private void UpdateVisual()
    {
        iItem = enhancePanel.iItem;
        EnhanceStatsPanel.SetInterfaceItem(iItem);
        InitExpSliderValue();
        UpdateMaxSliderValue();
        OnEnhanceItemChanged?.Invoke(this, EventArgs.Empty);
    }

    private void OnDestroy()
    {
        EnhancementMaterialContainer.OnUpgradeClick -= EnhancementMaterialContainer_OnUpgradeClick;

        if (enhancePanel != null)
        {
            enhancePanel.OnUpgradableItemChanged -= EnhancePanel_OnUpgradableItemChanged;
        }
    }
}
