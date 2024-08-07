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
    public IMapIconWidget iMapIconWidget { get; private set; }

    public MapIconAction mapIconAction { get; private set; }

    public event EventHandler<MapIconEvent> OnMapIconClick;

    private void Awake()
    {
        RT = GetComponent<RectTransform>();
    }

    public void SetMapIconWidget(IMapIconWidget IMapIconWidget, WorldMapBackground WorldMapBackground)
    {
        iMapIconWidget = IMapIconWidget;

        if (iMapIconWidget == null)
            return;

        worldMapBackground = WorldMapBackground;

        UnsubscribeEvent();
        mapIconAction = iMapIconWidget.AddMapIconActionComponent(this);
        SubscribeEvents();
        UpdateIconPosition();

        if (iMapIconWidget.GetMapIconTypeSO() == null)
            return;

        mapIconAction.SetIconSprite(iMapIconWidget.GetMapIconTypeSO().IconSprite);
    }

    private void UnsubscribeEvent()
    {
        if (mapIconAction == null)
            return;

        mapIconAction.OnMapIconChanged -= MapIconAction_OnMapIconChanged;
    }

    private void SubscribeEvents()
    {
        if (mapIconAction == null)
            return;

        mapIconAction.OnMapIconChanged += MapIconAction_OnMapIconChanged;
    }

    private void MapIconAction_OnMapIconChanged(object sender, EventArgs e)
    {
        IconImage.sprite = mapIconAction.GetImageSprite();
    }

    private void Update()
    {
        UpdateIconPosition();
        UpdateMapIconAction();
    }

    private void UpdateIconPosition()
    {
        if (iMapIconWidget == null)
            return;

        RT.anchoredPosition = worldMapBackground.GetMapUILocation(iMapIconWidget);
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
