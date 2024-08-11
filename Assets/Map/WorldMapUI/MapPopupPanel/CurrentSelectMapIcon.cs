using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public abstract class CurrentSelectMapIcon : MonoBehaviour
{
    protected MapPopupPanel mapPopupPanel;
    protected MapIcon mapIcon;

    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        if (mapPopupPanel != null)
            return;

        mapPopupPanel = GetComponentInParent<MapPopupPanel>(true);
        mapPopupPanel.OnMapIconChanged += MapPopupPanel_OnMapIconChanged;
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        mapIcon = mapPopupPanel.mapIcon;

        if (mapIcon == null || mapIcon.mapObject == null)
            return;

        gameObject.SetActive(IsVisible());

        if (gameObject.activeSelf && mapIcon.mapObject.GetMapIconTypeSO())
        {
            UpdateInformation();
        }
    }

    private void MapPopupPanel_OnMapIconChanged(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    protected abstract bool IsVisible();
    protected abstract void UpdateInformation();

    protected virtual void OnDestroy()
    {
        if (mapPopupPanel != null)
        {
            mapPopupPanel.OnMapIconChanged -= MapPopupPanel_OnMapIconChanged;
        }
    }
}
