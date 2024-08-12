using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MarkerPinContent : MonoBehaviour
{
    protected MarkerSelectedMapIcon markerSelectedMapIcon { get; private set; }
    private MapIcon markerIcon;
    protected PlayerMarkerIconData playerMarkerIconData;

    protected virtual void Awake()
    {
        Init();
    }

    public void Init()
    {
        if (markerSelectedMapIcon != null)
            return;

        markerSelectedMapIcon = GetComponentInParent<MarkerSelectedMapIcon>();
        markerSelectedMapIcon.OnMapIconChanged += OnMapIconChanged;
        UpdateMapIconInformation();
    }

    private void OnDestroy()
    {
        UnSubscribeEvents();
        if (markerSelectedMapIcon)
        {
            markerSelectedMapIcon.OnMapIconChanged -= OnMapIconChanged;
        }
    }

    private void UpdateMapIconInformation()
    {
        UnSubscribeEvents();
        markerIcon = markerSelectedMapIcon.mapIcon;
        SubscribeEvents();
        UpdateVisual();
    }

    private void OnMapIconChanged(object sender, System.EventArgs e)
    {
        UpdateMapIconInformation();
    }

    private void UpdateVisual()
    {
        if (markerIcon == null)
            return;

        MapIconData mapIconData = markerIcon.mapObject.mapIconData;
        playerMarkerIconData = mapIconData as PlayerMarkerIconData;

        gameObject.SetActive(playerMarkerIconData != null && IsContentVisible());
    }

    private void UnSubscribeEvents()
    {
        if (markerIcon == null || markerIcon.mapObject == null)
            return;

        MapIconData mapIconData = markerIcon.mapObject.mapIconData;

        if (mapIconData == null)
            return;

        mapIconData.OnMapIconChanged -= MapIconData_OnMapIconChanged;
    }

    private void SubscribeEvents()
    {
        if (markerIcon == null || markerIcon.mapObject == null)
            return;

        MapIconData mapIconData = markerIcon.mapObject.mapIconData;

        if (mapIconData == null)
            return;

        mapIconData.OnMapIconChanged += MapIconData_OnMapIconChanged;
    }

    private void MapIconData_OnMapIconChanged(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    protected abstract bool IsContentVisible();

}
