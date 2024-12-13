using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(WorldMapUI))]
[DisallowMultipleComponent]
public class PlaceMarker : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject m_MarkerPrefab;
    private WorldMapUI worldMapUI;

    private void Awake()
    {
        worldMapUI = GetComponent<WorldMapUI>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left || worldMapUI.worldMapBackground == null)
            return;

        WorldMapBackground worldMapBackground = worldMapUI.worldMapBackground;
        Vector2 mapMousePosition = eventData.position - (worldMapBackground.GetScreenSize() * 0.5f) + (worldMapBackground.GetMapSize() * 0.5f) - (worldMapBackground.MapRT.anchoredPosition + worldMapBackground.OffsetPositionCenter());
        Vector3 WorldPosition = worldMapUI.worldMapBackground.worldMap.GetWorldMapLocation(worldMapBackground.GetMapSize(), mapMousePosition);
        PlaceMarkerOnMap(WorldPosition);
    }

    private void PlaceMarkerOnMap(Vector3 WorldPosition)
    {
        WorldMapBackground worldMapBackground = worldMapUI.worldMapBackground;
        int TotalPlayerMarkers = worldMapBackground.worldMap.CountAllPlacedMarkers();

        if (TotalPlayerMarkers >= WorldMapManager.MAX_PIN)
            return;

        GameObject marker = Instantiate(m_MarkerPrefab, WorldPosition, Quaternion.identity);
    }
}
