using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhancePanel : MonoBehaviour
{
    private UpgradableItemContent UpgradableItemContent;
    public IItem iItem { get; private set; }

    public event EventHandler OnUpgradableItemChanged;

    private void Awake()
    {
        Init();
    }

    public void UpdateVisual()
    {
        Init();
        iItem = UpgradableItemContent.iItem;
        OnUpgradableItemChanged?.Invoke(this, EventArgs.Empty);
    }

    private void Init()
    {
        if (UpgradableItemContent != null)
            return;

        UpgradableItemContent = GetComponentInParent<UpgradableItemContent>();
    }
}
