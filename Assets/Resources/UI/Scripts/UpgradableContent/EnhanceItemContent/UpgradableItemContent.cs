using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradableItemContent : MonoBehaviour
{
    [SerializeField] private DetailsPanel DetailsPanel;
    [SerializeField] private EnhancePanel EnhancePanel;
    public IItem iItem { get; private set; }

    public void SetIItem(IItem IItem)
    {
        iItem = IItem;
        gameObject.SetActive(iItem != null);
        DetailsPanel.UpdateVisual();
        EnhancePanel.UpdateVisual();
    }
}
