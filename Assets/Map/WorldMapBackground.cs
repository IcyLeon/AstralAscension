using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldMapBackground : MonoBehaviour
{
    [SerializeField] private MapIconPrefabManagerSO MapIconPrefabManagerSO;
    public Dictionary<MapObject, MapIcon> IconsDictionary { get; private set; }
    [field: SerializeField] public RectTransform MapRT { get; private set; }
    [SerializeField] private Transform IconMapPivotTransform;
    public WorldMapManager worldMap { get; private set; }

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
            Debug.Log("World Map Manager not found!");
            return;
        }

        worldMap.OnMapObjectAdd += WorldMap_OnMapObjectAdd;
        worldMap.OnMapObjectRemove += WorldMap_OnMapObjectRemove;
        UpdateVisual();
    }

    private void WorldMap_OnMapObjectRemove(MapObject MapObject)
    {
        if (!IconsDictionary.TryGetValue(MapObject, out MapIcon mapIcon))
            return;

        IconsDictionary.Remove(MapObject);

        if (mapIcon == null)
            return;

        OnMapIconRemove?.Invoke(mapIcon);
        Destroy(mapIcon.gameObject);
    }

    private void UpdateVisual()
    {
        for (int i = IconsDictionary.Count - 1; i >= 0; i--)
        {
            WorldMap_OnMapObjectRemove(IconsDictionary.ElementAt(i).Key);
        }

        foreach (var IMapIconWidget in worldMap.MapObjectList)
        {
            WorldMap_OnMapObjectAdd(IMapIconWidget.Key);
        }
    }

    private void OnDestroy()
    {
        if (worldMap == null)
            return;

        worldMap.OnMapObjectAdd -= WorldMap_OnMapObjectAdd;
        worldMap.OnMapObjectRemove -= WorldMap_OnMapObjectRemove;
    }

    private void WorldMap_OnMapObjectAdd(MapObject mapObject)
    {
        if (IconsDictionary.ContainsKey(mapObject))
            return;

        GameObject prefab = MapIconPrefabManagerSO.GetMapIconPrefab(mapObject);
        MapIcon mapIcon = Instantiate(prefab, IconMapPivotTransform).GetComponent<MapIcon>();
        mapIcon.SetMapObject(mapObject, this);
        IconsDictionary.Add(mapObject, mapIcon);
        OnMapIconAdd?.Invoke(mapIcon);
    }


    public Vector3 GetMapUILocation(MapObject mapObject)
    {
        Vector3 MapWorldPosition = worldMap.GetMapLocation(mapObject.GetMapIconTransform());

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
