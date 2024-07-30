using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private MapIconAction mapIconAction;

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

        mapIconAction = iMapIconWidget.AddMapIconComponent(this);

        if (iMapIconWidget.GetMapIconTypeSO() == null)
            return;

        IconImage.sprite = iMapIconWidget.GetMapIconTypeSO().IconSprite;
    }

    private void Update()
    {
        UpdateIconPosition();
        UpdateMapIconAction();
    }

    private void UpdateIconPosition()
    {
        RT.anchoredPosition = worldMapBackground.GetMapUILocation(iMapIconWidget.GetMapIconTransform());
    }
    private void UpdateMapIconAction()
    {
        if (mapIconAction == null)
            return;

        mapIconAction.Update();
    }

    private void OnDestroy()
    {
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
