using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldMapUI : MapUI
{
    private MapPopupPanel mapPopupPanel;
    private Canvas canvas;

    public DragnDrop dragnDrop { get; private set; }

    // Zoom
    private Player player;
    private float m_TargetZoom;
    private float m_Zoom;
    private float zoomScale;

    private Vector2 PivotPoint;

    protected override void Awake()
    {
        base.Awake();

        m_TargetZoom = m_Zoom = 1.75f;
        zoomScale = 0.5f;

        canvas = GetComponentInParent<Canvas>();
        dragnDrop = GetComponent<DragnDrop>();

        mapPopupPanel = GetComponentInChildren<MapPopupPanel>(true);
        mapPopupPanel.SetMapUI(this);

    }

    private void Start()
    {
        player = worldMapBackground.player;

        player.PlayerController.mapInputAction.Zoom.performed += Zoom_performed;

        UpdateVisual();
    }

    protected override void OnMapIconRemove(MapIcon mapIcon)
    {
        base.OnMapIconRemove(mapIcon);
        mapIcon.OnMapIconClick -= MapIcon_OnMapIconClick;
    }

    protected override void OnMapIconAdd(MapIcon mapIcon)
    {
        base.OnMapIconAdd(mapIcon);
        mapIcon.OnMapIconClick += MapIcon_OnMapIconClick;
    }

    private void Zoom_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        m_TargetZoom += player.PlayerController.mapInputAction.Zoom.ReadValue<Vector2>().y * zoomScale;
        m_TargetZoom = Mathf.Clamp(m_TargetZoom, 0.8f, 3.5f);
    }

    private void UpdateZoom()
    {
        m_Zoom = Mathf.SmoothStep(m_Zoom, m_TargetZoom, Time.deltaTime * 15f);
        worldMapBackground.MapRT.sizeDelta = worldMapBackground.GetOriginalMapSize() * m_Zoom;
    }

    private void UpdateVisual()
    {
        foreach (var iconDictionaryKeyPair in worldMapBackground.IconsDictionary)
        {
            OnMapIconRemove(iconDictionaryKeyPair.Value);
        }

        foreach (var iconDictionaryKeyPair in worldMapBackground.IconsDictionary)
        {
            OnMapIconAdd(iconDictionaryKeyPair.Value);
        }
    }

    private void MapIcon_OnMapIconClick(object sender, MapIconEvent MapIconEvent)
    {
        mapPopupPanel.SetMapIcon(MapIconEvent.MapIcon);
    }

    private void OnEnable()
    {
        dragnDrop.OnDragEvent += DragnDrop_OnDragEvent;
    }

    private void OnDisable()
    {
        dragnDrop.OnDragEvent -= DragnDrop_OnDragEvent;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        player.PlayerController.uiInputAction.Map.performed -= Zoom_performed;
        dragnDrop.OnDragEvent -= DragnDrop_OnDragEvent;
    }

    private void Update()
    {
        UpdateZoom();
    }

    private void DragnDrop_OnDragEvent(object sender, DragEvent DE)
    {
        if (DE.eventData.button != PointerEventData.InputButton.Left)
            return;

        RectTransform rt = worldMapBackground.MapRT;
        rt.anchoredPosition += DE.eventData.delta / canvas.scaleFactor;


        Vector2 clampedanchoredPosition = rt.anchoredPosition + worldMapBackground.OffsetPositionCenter();
        Vector2 Position = (clampedanchoredPosition - worldMapBackground.GetMapSize() * 0.5f) * -1f;
        PivotPoint = Position / worldMapBackground.GetMapSize();
        worldMapBackground.MapRT.pivot = PivotPoint;


        clampedanchoredPosition.x = Mathf.Clamp(clampedanchoredPosition.x, -worldMapBackground.GetMapSize().x / 2f, worldMapBackground.GetMapSize().x / 2f);
        clampedanchoredPosition.y = Mathf.Clamp(clampedanchoredPosition.y, -worldMapBackground.GetMapSize().y / 2f, worldMapBackground.GetMapSize().y / 2f);
        rt.anchoredPosition = clampedanchoredPosition - worldMapBackground.OffsetPositionCenter();
    }
}
