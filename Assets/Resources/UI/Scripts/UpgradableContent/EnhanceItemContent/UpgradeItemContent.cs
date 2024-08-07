using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeItemContent : MonoBehaviour
{
    private EnhancementPanel[] EnhancementPanels;

    public IItem iItem { get; private set; }

    public event EventHandler OnUpgradableItemChanged;

    private void Awake()
    {
        EnhancementPanels = GetComponentsInChildren<EnhancementPanel>(true);

        foreach (var enhancementPanel in EnhancementPanels)
        {
            enhancementPanel.Init();
        }
    }

    public void SetIItem(IItem IItem)
    {
        iItem = IItem;

        gameObject.SetActive(iItem != null);

        OnUpgradableItemChanged?.Invoke(this, EventArgs.Empty);
    }
}
