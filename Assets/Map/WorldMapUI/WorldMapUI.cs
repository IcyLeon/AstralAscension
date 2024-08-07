using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[DisallowMultipleComponent]
public class WorldMapUI : MonoBehaviour
{
    private MapPopupPanel mapPopupPanel;
    private Canvas canvas;

    private WorldMapBackground WorldMapBackground;

    private WorldMapManager worldMap;

    public DragnDrop dragnDrop { get; private set; }

    // Zoom
    private Player player;
    private float m_TargetZoom;
    private float m_Zoom;
    private float zoomScale;

    private Vector2 PivotPoint;

    private void Awake()
    {
        m_TargetZoom = m_Zoom = 1.75f;
        zoomScale = 0.5f;

        canvas = GetComponentInParent<Canvas>();
        dragnDrop = GetComponent<DragnDrop>();

        WorldMapBackground = GetComponentInChildren<WorldMapBackground>();
        mapPopupPanel = GetComponentInChildren<MapPopupPanel>(true);

        WorldMapBackground.OnMapIconAdd += WorldMapBackground_OnMapIconAdd;
        WorldMapBackground.OnMapIconRemove += WorldMapBackground_OnMapIconRemove;

    }

    public WorldMapBackground GetWorldMapBackground()
    {
        return WorldMapBackground;
    }

    private void Start()
    {
        worldMap = WorldMapManager.instance;
        if (worldMap == null)
        {
            Debug.LogError("World Map Manager is not found!");
            return;
        }

        player = WorldMapBackground.player;

        player.PlayerController.mapInputAction.Zoom.performed += Zoom_performed;

        UpdateVisual();
    }

    private void WorldMapBackground_OnMapIconRemove(MapIcon mapIcon)
    {
        mapIcon.OnMapIconClick -= MapIcon_OnMapIconClick;
    }

    private void WorldMapBackground_OnMapIconAdd(MapIcon mapIcon)
    {
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
        WorldMapBackground.MapRT.sizeDelta = WorldMapBackground.GetOriginalMapSize() * m_Zoom;
    }

    private void UpdateVisual()
    {
        foreach (var iconDictionaryKeyPair in WorldMapBackground.IconsDictionary)
        {
            WorldMapBackground_OnMapIconRemove(iconDictionaryKeyPair.Value);
        }

        foreach (var iconDictionaryKeyPair in WorldMapBackground.IconsDictionary)
        {
            WorldMapBackground_OnMapIconAdd(iconDictionaryKeyPair.Value);
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

    private void OnDestroy()
    {
        if (WorldMapBackground != null) 
        {
            WorldMapBackground.OnMapIconAdd -= WorldMapBackground_OnMapIconAdd;
            WorldMapBackground.OnMapIconRemove -= WorldMapBackground_OnMapIconRemove;
        }

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

        RectTransform rt = WorldMapBackground.MapRT;
        rt.anchoredPosition += DE.eventData.delta / canvas.scaleFactor;


        Vector2 clampedanchoredPosition = rt.anchoredPosition + WorldMapBackground.OffsetPositionCenter();
        Vector2 Position = (clampedanchoredPosition - WorldMapBackground.GetMapSize() * 0.5f) * -1f;
        PivotPoint = Position / WorldMapBackground.GetMapSize();
        WorldMapBackground.MapRT.pivot = PivotPoint;


        clampedanchoredPosition.x = Mathf.Clamp(clampedanchoredPosition.x, -WorldMapBackground.GetMapSize().x / 2f, WorldMapBackground.GetMapSize().x / 2f);
        clampedanchoredPosition.y = Mathf.Clamp(clampedanchoredPosition.y, -WorldMapBackground.GetMapSize().y / 2f, WorldMapBackground.GetMapSize().y / 2f);
        rt.anchoredPosition = clampedanchoredPosition - WorldMapBackground.OffsetPositionCenter();
    }
}
