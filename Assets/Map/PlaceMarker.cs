using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(WorldMapUI))]
[DisallowMultipleComponent]
public class PlaceMarker : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject m_MarkerPrefab;
    private WorldMapUI WorldMapUI;
    private WorldMapManager worldMap;

    private void Awake()
    {
        WorldMapUI = GetComponent<WorldMapUI>();
    }
    private void Start()
    {
        worldMap = WorldMapManager.instance;
        if (worldMap == null)
        {
            Debug.LogError("World Map Manager is not found!");
            return;
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        if (WorldMapUI == null)
        {
            Debug.LogError("World Map UI not found!");
            return;
        }

        WorldMapBackground worldMapBackground = WorldMapUI.WorldMapBackground;
        Vector2 mapMousePosition = eventData.position - (worldMapBackground.GetScreenSize() * 0.5f) + (worldMapBackground.GetMapSize() * 0.5f) - worldMapBackground.MapRT.anchoredPosition;
        Vector3 WorldPosition = worldMap.GetWorldMapLocation(worldMapBackground.GetMapSize(), mapMousePosition);
        PlaceMarkerOnMap(WorldPosition);
    }

    private void PlaceMarkerOnMap(Vector3 WorldPosition)
    {
        GameObject marker = Instantiate(m_MarkerPrefab, WorldPosition, Quaternion.identity);
    }
}
