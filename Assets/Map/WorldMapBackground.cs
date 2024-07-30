using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorldMapBackground : MonoBehaviour
{
    [SerializeField] private GameObject MapIconPrefab;
    [field: SerializeField] public RectTransform MapRT { get; private set; }
    [SerializeField] private Transform IconMapPivotTransform;
    private WorldMapManager worldMap;

    private void Start()
    {
        worldMap = WorldMapManager.instance;
        if (worldMap == null)
        {
            Debug.LogError("World Map Manager is not found!");
            return;
        }
    }

    public MapIcon SpawnMapIcon(IMapIconWidget iMapIconWidget)
    {
        MapIcon mapIcon = Instantiate(MapIconPrefab, IconMapPivotTransform).GetComponent<MapIcon>();
        mapIcon.SetMapIconWidget(iMapIconWidget, this);
        return mapIcon;
    }

    public Vector3 GetMapUILocation(Transform transform)
    {
        Vector3 MapWorldPosition = worldMap.GetMapLocation(transform);

        return new Vector3(MapWorldPosition.x / worldMap.GetWorldMapWidthRatio(GetMapSize().x),
            MapWorldPosition.z / worldMap.GetWorldMapWidthRatio(GetMapSize().y), 0);
    }

    public Vector2 GetMapSize()
    {
        return new Vector2(MapRT.rect.width, MapRT.rect.height);
    }
    public Vector2 GetScreenSize()
    {
        return new Vector2(Screen.width, Screen.height);
    }

}
