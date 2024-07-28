using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class EnhancementManager : MonoBehaviour
{
    [SerializeField] private Slider ExpSlider;

    [Header("Enhancement Reference")]
    [SerializeField] private EnhancementMaterialContainer EnhancementMaterialContainer;
    [SerializeField] private EnhanceStatsPanel EnhanceStatsPanel;
    public IItem iItem { get; private set; }
    private EnhancePanel enhancePanel;
    private Coroutine enhanceCoroutine;

    public event EventHandler OnEnhanceItemChanged;

    private void Awake()
    {
        EnhancementMaterialContainer.OnUpgradeClick += EnhancementMaterialContainer_OnUpgradeClick;

        enhancePanel = GetComponentInParent<EnhancePanel>();
        if (enhancePanel != null)
        {
            enhancePanel.OnUpgradableItemChanged += EnhancePanel_OnUpgradableItemChanged;
        }
    }

    private void EnhancementMaterialContainer_OnUpgradeClick(object sender, EventArgs e)
    {
        UpgradeItem();
    }


    private void UpgradeItem()
    {
        UpgradableItems upgradableItems = iItem as UpgradableItems;

        if (upgradableItems == null)
            return;

        upgradableItems.Upgrade();
    }

    private void EnhancePanel_OnUpgradableItemChanged(object sender, System.EventArgs e)
    {
        iItem = enhancePanel.iItem;
        EnhanceStatsPanel.SetInterfaceItem(iItem);
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
