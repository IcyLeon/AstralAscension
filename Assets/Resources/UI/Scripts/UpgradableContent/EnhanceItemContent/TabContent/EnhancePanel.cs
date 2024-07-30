using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhancePanel : MonoBehaviour
{
    private EnhanceItemContent EnhanceItemContent;
    public IItem iItem { get; private set; }

    public event EventHandler OnUpgradableItemChanged;

    private void Awake()
    {
        Init();
    }

    public void UpdateVisual()
    {
        Init();
        iItem = EnhanceItemContent.iItem;
        OnUpgradableItemChanged?.Invoke(this, EventArgs.Empty);
    }

    private void Init()
    {
        if (EnhanceItemContent != null)
            return;

        EnhanceItemContent = GetComponentInParent<EnhanceItemContent>();
    }
}
