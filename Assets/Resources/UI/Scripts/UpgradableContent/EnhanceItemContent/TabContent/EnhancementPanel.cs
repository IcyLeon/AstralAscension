using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnhancementPanel : MonoBehaviour
{
    private UpgradeItemContent UpgradeItemContent;
    public event Action OnUpgradableItemChanged;
    public IEXP iEXPEntity { get; private set; }

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        if (UpgradeItemContent != null)
            return;

        UpgradeItemContent = GetComponentInParent<UpgradeItemContent>(true);
        UpgradeItemContent.OnUpgradableItemChanged += EnhanceItemContent_OnUpgradableItemChanged;
    }

    private void EnhanceItemContent_OnUpgradableItemChanged(object sender, EventArgs e)
    {
        UpdateVisual();
    }
    protected virtual void UpdateVisual()
    {
        iEXPEntity = UpgradeItemContent.iEXPEntity;
        OnUpgradableItemChanged?.Invoke();
    }

    private void OnDestroy()
    {
        if (UpgradeItemContent != null)
        {
            UpgradeItemContent.OnUpgradableItemChanged -= EnhanceItemContent_OnUpgradableItemChanged;
        }
    }
}
