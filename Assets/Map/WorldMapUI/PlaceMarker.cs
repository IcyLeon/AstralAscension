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

        if (worldMapUI == null)
        {
            Debug.LogError("World Map UI not found!");
            return;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left || worldMapUI.worldMapBackground == null 
            || worldMapUI.dragnDrop.GetDragObject() != null)
            return;

        WorldMapBackground worldMapBackground = worldMapUI.worldMapBackground;
        Vector2 mapMousePosition = eventData.position - (worldMapBackground.GetScreenSize() * 0.5f) + (worldMapBackground.GetMapSize() * 0.5f) - (worldMapBackground.MapRT.anchoredPosition + worldMapBackground.OffsetPositionCenter());
        Vector3 WorldPosition = worldMapUI.worldMapBackground.worldMap.GetWorldMapLocation(worldMapBackground.GetMapSize(), mapMousePosition);
        PlaceMarkerOnMap(WorldPosition);
    }

    private void PlaceMarkerOnMap(Vector3 WorldPosition)
    {
        GameObject marker = Instantiate(m_MarkerPrefab, WorldPosition, Quaternion.identity);
    }
}
