using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UpgradeEvent : EventArgs
{
    public float TotalEXP;
    public int currentLevel;
    public float interpolation;
}

[DisallowMultipleComponent]
public class EnhancementManager : MonoBehaviour
{
    [SerializeField] private GameObject ButtonMask;
    private EnhancePanel enhancePanel;
    private EnhancementMaterialContainer EnhancementMaterialContainer;
    public UpgradableItems upgradableItem { get; private set; }
    public event Action OnEnhanceItemChanged;
    public event Action OnItemUpgrade;
    public event EventHandler<UpgradeEvent> OnItemUpgradeProcessing;
    public event Action<int> OnSlotChanged;

    private void Awake()
    {
        enhancePanel = GetComponentInParent<EnhancePanel>(true);
        enhancePanel.OnUpgradableItemChanged += EnhancePanel_OnUpgradableItemChanged;

        EnhancementMaterialContainer = GetComponentInChildren<EnhancementMaterialContainer>();
        EnhancementMaterialContainer.OnUpgradeClick += EnhancementMaterialContainer_OnUpgradeClick;
        EnhancementMaterialContainer.OnSlotChanged += EnhancementMaterialContainer_OnSlotChanged;
        UpdateVisual();

    }


    private void EnhancementMaterialContainer_OnSlotChanged(int IncreaseEXP)
    {
        OnSlotChanged?.Invoke(IncreaseEXP);
    }

    private void EnhancementMaterialContainer_OnUpgradeClick(int IncreaseEXP)
    {
        int currentEXP = upgradableItem.currentEXP;
        int currentLevel = upgradableItem.level;
        upgradableItem.IncreaseEXP(IncreaseEXP);
        StartCoroutine(UpgradingEnumerator(currentEXP, currentLevel, IncreaseEXP));
    }

    private IEnumerator UpgradingEnumerator(int CurrentEXP, int CurrentLevel, int IncreaseEXP)
    {
        float elapseTime = 0f;
        float duration = 1.25f;

        ButtonMask.SetActive(true);

        UpgradeEvent upgradeEvent = new UpgradeEvent();
        upgradeEvent.TotalEXP = CurrentEXP + IncreaseEXP;
        upgradeEvent.currentLevel = CurrentLevel;

        do
        {
            upgradeEvent.interpolation = elapseTime / duration;
            OnItemUpgradeProcessing?.Invoke(this, upgradeEvent);
            elapseTime += Time.unscaledDeltaTime;
            yield return null;

        } while (elapseTime <= duration);

        OnItemUpgrade?.Invoke();
        ButtonMask.SetActive(false);
    }

    private void EnhancePanel_OnUpgradableItemChanged()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        UnsubscribeEvents();
        upgradableItem = enhancePanel.upgradableItem;
        SubscribeEvents();
        OnEnhanceItemChanged?.Invoke();
    }

    private void UnsubscribeEvents()
    {
        if (upgradableItem == null)
            return;

        upgradableItem.OnUpgradeIEXP -= IEXPEntity_OnUpgradeIEXP;
    }

    private void SubscribeEvents()
    {
        if (upgradableItem == null)
            return;

        upgradableItem.OnUpgradeIEXP += IEXPEntity_OnUpgradeIEXP;
    }

    private void IEXPEntity_OnUpgradeIEXP()
    {
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
        enhancePanel.OnUpgradableItemChanged -= EnhancePanel_OnUpgradableItemChanged;
        EnhancementMaterialContainer.OnUpgradeClick -= EnhancementMaterialContainer_OnUpgradeClick;
        EnhancementMaterialContainer.OnSlotChanged -= EnhancementMaterialContainer_OnSlotChanged;
    }
}
