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
    [SerializeField] private Image IconImage;
    public RectTransform RT { get; private set; }
    public WorldMapBackground worldMapBackground { get; private set; }
    public MapObject mapObject { get; private set; }
    public Player player { get; private set; }

    public event EventHandler<MapIconEvent> OnMapIconClick;

    private void Awake()
    {
        RT = GetComponent<RectTransform>();
    }

    public virtual void SetMapObject(Player Player, MapObject MapObject, WorldMapBackground WorldMapBackground)
    {
        worldMapBackground = WorldMapBackground;
        UnsubscribeEvent();
        mapObject = MapObject;
        player = Player;
        SubscribeEvents();
    }

    private void UnsubscribeEvent()
    {
        if (mapObject == null || mapObject.mapIconData == null)
            return;

        mapObject.mapIconData.OnMapIconChanged -= MapIconAction_OnMapIconChanged;
    }

    private void SubscribeEvents()
    {
        if (mapObject == null || mapObject.mapIconData == null)
            return;

        mapObject.mapIconData.OnMapIconChanged += MapIconAction_OnMapIconChanged;
        UpdateIconPosition();
        UpdateVisual();
    }

    private void MapIconAction_OnMapIconChanged(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (mapObject == null || mapObject.mapIconData == null)
            return;

        IconImage.sprite = mapObject.mapIconData.mapIconSprite;
    }    

    protected virtual void Update()
    {
        UpdateIconPosition();
    }

    private void UpdateIconPosition()
    {
        if (mapObject == null)
            return;

        RT.anchoredPosition = worldMapBackground.GetMapUILocation(mapObject);
    }

    protected virtual void OnDestroy()
    {
        UnsubscribeEvent();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnMapIconClick?.Invoke(this, new MapIconEvent
        {
            MapIcon = this
        });
    }
}
