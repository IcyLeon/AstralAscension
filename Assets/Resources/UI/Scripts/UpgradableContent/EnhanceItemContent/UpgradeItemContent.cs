using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeItemContent : MonoBehaviour
{
    private EnhancementPanel[] EnhancementPanels;

    public IEXP iEXPEntity { get; private set; }

    public event EventHandler OnUpgradableItemChanged;

    private void Awake()
    {
        EnhancementPanels = GetComponentsInChildren<EnhancementPanel>(true);

        foreach (var enhancementPanel in EnhancementPanels)
        {
            enhancementPanel.Init();
        }
    }

    public void SetIItem(IEXP IExpEntity)
    {
        iEXPEntity = IExpEntity;

        gameObject.SetActive(iEXPEntity != null);

        OnUpgradableItemChanged?.Invoke(this, EventArgs.Empty);
    }
}
