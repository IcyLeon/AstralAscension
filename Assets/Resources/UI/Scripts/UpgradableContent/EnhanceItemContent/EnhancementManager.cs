using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UpgradeEvent : EventArgs
{
    public int totalEXP;
    public int currentLevel;
    public float duration;
}

[DisallowMultipleComponent]
public class EnhancementManager : MonoBehaviour
{
    [SerializeField] private GameObject ButtonMask;
    private EnhancePanel enhancePanel;
    private EnhancementMaterialContainer enhancementMaterialContainer;
    private Coroutine enhanceCoroutine;
    public UpgradableItems upgradableItem { get; private set; }
    public event Action OnEnhanceItemChanged;
    public event EventHandler<UpgradeEvent> OnItemUpgradeProcessing;
    public event Action<int> OnSlotChanged;

    private void Awake()
    {
        enhancePanel = GetComponentInParent<EnhancePanel>(true);
        enhancePanel.OnUpgradableItemChanged += EnhancePanel_OnUpgradableItemChanged;
        enhancementMaterialContainer = GetComponentInChildren<EnhancementMaterialContainer>();
        enhancementMaterialContainer.OnUpgradeClick += EnhancementMaterialContainer_OnUpgradeClick;
        enhancementMaterialContainer.OnSlotChanged += EnhancementMaterialContainer_OnSlotChanged;
        UpdateVisual();

    }
    
    private bool IsUpgrading()
    {
        return enhanceCoroutine != null;
    }

    private void EnhancementMaterialContainer_OnSlotChanged(int IncreaseEXP)
    {
        if (IsUpgrading())
            return;

        OnSlotChanged?.Invoke(IncreaseEXP);
    }

    private void EnhancementMaterialContainer_OnUpgradeClick(int IncreaseEXP)
    {
        if (IsUpgrading())
        {
            StopCoroutine(enhanceCoroutine);
        }

        int currentEXP = upgradableItem.currentEXP;
        int currentLevel = upgradableItem.level;
        upgradableItem.AddEXP(IncreaseEXP);
        enhanceCoroutine = StartCoroutine(UpgradingEnumerator(currentEXP, currentLevel, IncreaseEXP));
    }

    private IEnumerator UpgradingEnumerator(int CurrentEXP, int CurrentLevel, int IncreaseEXP)
    {
        float duration = 1.0f;

        ButtonMask.SetActive(true);

        OnItemUpgradeProcessing?.Invoke(this, new UpgradeEvent
        {
            totalEXP = CurrentEXP + IncreaseEXP,
            currentLevel = CurrentLevel,
            duration = duration    
        });

        yield return new WaitForSecondsRealtime(duration);
        ButtonMask.SetActive(false);
        enhanceCoroutine = null;
    }

    private void EnhancePanel_OnUpgradableItemChanged()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        upgradableItem = enhancePanel.upgradableItem;
        OnEnhanceItemChanged?.Invoke();
    }


    private void OnDestroy()
    {
        enhancePanel.OnUpgradableItemChanged -= EnhancePanel_OnUpgradableItemChanged;
        enhancementMaterialContainer.OnUpgradeClick -= EnhancementMaterialContainer_OnUpgradeClick;
        enhancementMaterialContainer.OnSlotChanged -= EnhancementMaterialContainer_OnSlotChanged;
    }
}
