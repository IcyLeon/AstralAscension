using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapPopupPanel : MonoBehaviour
{
    public event EventHandler OnMapIconChanged;
    public MapUI mapUI { get; private set; }

    private CurrentSelectMapIcon[] MapIconPanels;

    public MapIcon mapIcon { get; private set; }

    private void Awake()
    {
        MapIconPanels = GetComponentsInChildren<CurrentSelectMapIcon>(true);

        foreach (var MapIconPanel in MapIconPanels)
        {
            MapIconPanel.Init();
        }
    }

    public void SetMapUI(MapUI MapUI)
    {
        UnsubscribeEvents();
        mapUI = MapUI;
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        if (mapUI == null || mapUI.worldMapBackground == null)
            return;

        mapUI.worldMapBackground.OnMapIconAdd += WorldMapBackground_OnMapIconAdd;
    }

    private void UnsubscribeEvents()
    {
        if (mapUI == null || mapUI.worldMapBackground == null)
            return;

        mapUI.worldMapBackground.OnMapIconAdd -= WorldMapBackground_OnMapIconAdd;
    }

    private void WorldMapBackground_OnMapIconAdd(MapIcon MapIcon)
    {
        PlayerMarkerWorldObject playerMarkerWorld = MapIcon.mapObject as PlayerMarkerWorldObject;

        if (playerMarkerWorld == null)
            return;

        SetMapIcon(MapIcon);
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    public void TogglePanel(bool toggleStatus)
    {
        gameObject.SetActive(toggleStatus);
    }

    public void SetMapIcon(MapIcon MapIcon)
    {
        mapIcon = MapIcon;

        TogglePanel(mapIcon != null);

        OnMapIconChanged?.Invoke(this, EventArgs.Empty);
    }
}
