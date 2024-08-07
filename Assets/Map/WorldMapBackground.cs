using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldMapBackground : MonoBehaviour
{
    [SerializeField] private GameObject MapIconPrefab;
    public Dictionary<IMapIconWidget, MapIcon> IconsDictionary { get; private set; }
    [field: SerializeField] public RectTransform MapRT { get; private set; }
    [SerializeField] private Transform IconMapPivotTransform;
    private WorldMapManager worldMap;

    private Vector2 originalMapSize;

    public delegate void OnMapIconChanged(MapIcon mapIcon);
    public event OnMapIconChanged OnMapIconAdd;
    public event OnMapIconChanged OnMapIconRemove;
    public Player player { get; private set; }

    private void Awake()
    {
        player = GetComponentInParent<Player>();

        IconsDictionary = new();
        originalMapSize = GetMapSize();
    }

    private void Start()
    {
        worldMap = WorldMapManager.instance;
        if (worldMap == null)
        {
            Debug.LogError("World Map Manager is not found!");
            return;
        }

        worldMap.OnMapObjectAdd += WorldMap_OnMapObjectAdd;
        worldMap.OnMapObjectRemove += WorldMap_OnMapObjectRemove;
        UpdateVisual();
    }

    private void WorldMap_OnMapObjectRemove(IMapIconWidget IMapIconWidget)
    {
        if (!IconsDictionary.TryGetValue(IMapIconWidget, out MapIcon mapIcon))
            return;

        IconsDictionary.Remove(IMapIconWidget);
        OnMapIconRemove?.Invoke(mapIcon);
        Destroy(mapIcon.gameObject);

    }

    private void UpdateVisual()
    {
        for (int i = IconsDictionary.Count - 1; i >= 0; i--)
        {
            WorldMap_OnMapObjectRemove(IconsDictionary.ElementAt(i).Key);
        }

        foreach (var IMapIconWidget in worldMap.IMapIconWidgetList)
        {
            WorldMap_OnMapObjectAdd(IMapIconWidget);
        }
    }

    private void OnDestroy()
    {
        worldMap.OnMapObjectAdd -= WorldMap_OnMapObjectAdd;
        worldMap.OnMapObjectRemove -= WorldMap_OnMapObjectRemove;
    }

    private void WorldMap_OnMapObjectAdd(IMapIconWidget iMapIconWidget)
    {
        if (IconsDictionary.ContainsKey(iMapIconWidget))
            return;

        MapIcon mapIcon = Instantiate(MapIconPrefab, IconMapPivotTransform).GetComponent<MapIcon>();
        mapIcon.SetMapIconWidget(iMapIconWidget, this);
        IconsDictionary.Add(iMapIconWidget, mapIcon);
        OnMapIconAdd?.Invoke(mapIcon);
    }


    public Vector3 GetMapUILocation(IMapIconWidget mapIconWidget)
    {
        Vector3 MapWorldPosition = worldMap.GetMapLocation(mapIconWidget.GetMapIconTransform());

        return new Vector3(MapWorldPosition.x / worldMap.GetWorldMapWidthRatio(GetMapSize().x),
            MapWorldPosition.z / worldMap.GetWorldMapWidthRatio(GetMapSize().y), 0);
    }

    public Vector2 GetMapSize()
    {
        return new Vector2(MapRT.rect.width, MapRT.rect.height);
    }

    public Vector2 OffsetPositionCenter()
    {
        return (GetMapSize() * 0.5f) - MapRT.pivot * GetMapSize();
    }

    public Vector2 GetOriginalMapSize()
    {
        return originalMapSize;
    }

    public Vector2 GetScreenSize()
    {
        return new Vector2(Screen.width, Screen.height);
    }

}
