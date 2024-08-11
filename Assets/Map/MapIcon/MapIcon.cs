using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapIconEvent : EventArgs
{
    public MapIcon MapIcon;
}

public class MapIcon : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] protected Image IconImage;
    public RectTransform RT { get; private set; }
    public WorldMapBackground worldMapBackground { get; private set; }
    public MapObject mapObject { get; private set; }
    public MapIconAction mapIconAction { get; private set; } // update the icon gameobject transform etc.

    public event EventHandler<MapIconEvent> OnMapIconClick;

    private void Awake()
    {
        RT = GetComponent<RectTransform>();
    }

    public void SetMapObject(MapObject MapObject, WorldMapBackground WorldMapBackground)
    {
        worldMapBackground = WorldMapBackground;
        UnsubscribeEvent();
        mapObject = MapObject;
        mapIconAction = mapObject.GetMapIconActionComponent(this);
        SubscribeEvents();
    }

    private void UnsubscribeEvent()
    {
        if (mapIconAction == null || mapIconAction.mapIconData == null)
            return;

        mapIconAction.mapIconData.OnMapIconChanged -= MapIconAction_OnMapIconChanged;
    }

    private void SubscribeEvents()
    {
        if (mapIconAction == null || mapIconAction.mapIconData == null)
            return;

        mapIconAction.mapIconData.OnMapIconChanged += MapIconAction_OnMapIconChanged;
        UpdateIconPosition();
        UpdateVisual();
    }

    private void MapIconAction_OnMapIconChanged(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        MapIconTypeSO MapIconTypeSO = mapIconAction.mapIconData.GetMapIconTypeSO();

        if (MapIconTypeSO == null)
            return;

        IconImage.sprite = MapIconTypeSO.IconSprite;
    }    

    private void Update()
    {
        UpdateIconPosition();
        UpdateMapIconAction();
    }

    private void UpdateIconPosition()
    {
        if (mapObject == null)
            return;

        RT.anchoredPosition = worldMapBackground.GetMapUILocation(mapObject);
    }
    private void UpdateMapIconAction()
    {
        if (mapIconAction == null)
            return;

        mapIconAction.Update();
    }

    private void OnDestroy()
    {
        UnsubscribeEvent();

        if (mapIconAction == null)
            return;

        mapIconAction.OnDestroy();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnMapIconClick?.Invoke(this, new MapIconEvent
        {
            MapIcon = this
        });
    }
}
