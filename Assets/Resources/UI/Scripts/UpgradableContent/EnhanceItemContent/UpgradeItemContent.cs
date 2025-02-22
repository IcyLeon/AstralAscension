using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeItemContent : MonoBehaviour
{
    private EnhancementPanel[] EnhancementPanels;

    public UpgradableItems upgradableItem { get; private set; }

    public event EventHandler OnUpgradableItemChanged;

    private void Awake()
    {
        EnhancementPanels = GetComponentsInChildren<EnhancementPanel>(true);

        foreach (var enhancementPanel in EnhancementPanels)
        {
            enhancementPanel.Init();
        }
    }

    public void SetUpgradableItem(UpgradableItems UpgradableItems)
    {
        upgradableItem = UpgradableItems;

        gameObject.SetActive(upgradableItem != null);

        OnUpgradableItemChanged?.Invoke(this, EventArgs.Empty);
    }
}
