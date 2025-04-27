using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldMapUI : MapUI, IDragHandler
{
    private MapPopupPanel mapPopupPanel;
    private Canvas canvas;

    // Zoom
    private UIController uiController;
    private float m_TargetZoom;
    private float m_Zoom;
    private float zoomScale;

    private Vector2 PivotPoint;

    public override void Init()
    {
        base.Init();

        canvas = GetComponentInParent<Canvas>();

        m_TargetZoom = m_Zoom = 1.75f;
        zoomScale = 0.5f;

        mapPopupPanel = GetComponentInChildren<MapPopupPanel>(true);
        mapPopupPanel.SetMapUI(this);

    }

    private void Start()
    {
        uiController = UIController.instance;

        uiController.mapInputAction.Zoom.performed += Zoom_performed;

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
        m_TargetZoom += uiController.mapInputAction.Zoom.ReadValue<Vector2>().y * zoomScale;
        m_TargetZoom = Mathf.Clamp(m_TargetZoom, 0.8f, 3.5f);
    }

    private void UpdateZoom()
    {
        m_Zoom = Mathf.SmoothStep(m_Zoom, m_TargetZoom, Time.deltaTime * 15f);
        worldMapBackground.MapRT.sizeDelta = worldMapBackground.GetOriginalMapSize() * m_Zoom;
    }

    private void UpdateVisual()
    {
        foreach (var mapIcon in worldMapBackground.IconsDictionary.Values)
        {
            OnMapIconAdd(mapIcon);
        }
    }

    private void MapIcon_OnMapIconClick(object sender, MapIconEvent MapIconEvent)
    {
        mapPopupPanel.SetMapIcon(MapIconEvent.MapIcon);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        uiController.uiInputAction.Map.performed -= Zoom_performed;
    }

    private void Update()
    {
        UpdateZoom();
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransform rt = worldMapBackground.MapRT;
        rt.anchoredPosition += eventData.delta / canvas.scaleFactor;

        Vector2 clampedanchoredPosition = rt.anchoredPosition + worldMapBackground.OffsetPositionCenter();
        Vector2 Position = (worldMapBackground.GetMapSize() * 0.5f) - clampedanchoredPosition;
        PivotPoint = Position / worldMapBackground.GetMapSize();
        worldMapBackground.MapRT.pivot = PivotPoint;
        clampedanchoredPosition = Vector3Handler.Vector3Clamp(clampedanchoredPosition, -worldMapBackground.GetMapSize() * 0.5f, worldMapBackground.GetMapSize() * 0.5f);
        rt.anchoredPosition = clampedanchoredPosition - worldMapBackground.OffsetPositionCenter();
    }
}
