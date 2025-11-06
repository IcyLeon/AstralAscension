using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class WorldMapManager : MonoBehaviour
{
    public const int MAX_PIN = 200;

    public Dictionary<MapObject, MapIconData> MapObjectList { get; private set; }

    public static WorldMapManager instance { get; private set; }

    [SerializeField] private Transform UpperLeft;
    [SerializeField] private Transform BottomRight;

    public delegate void OnMapIconEvent(MapObject mapObject);
    public event OnMapIconEvent OnMapObjectAdd;
    public event OnMapIconEvent OnMapObjectRemove;

    private WorldMapCamera worldMapCamera;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Init();
        CenterCamera();
    }

    public int CountAllPlacedMarkers()
    {
        int count = 0;

        foreach(var MapObject in MapObjectList.Keys)
        {
            MapIconData MapIconData = MapObjectList[MapObject];

            if (MapObject is PlayerMarkerWorldObject && MapIconData.IsConfirmedPlaced())
                count++;
        }

        return count;
    }


    private void CenterCamera()
    {
        if (worldMapCamera == null)
            return;

        float x = UpperLeft.transform.position.x + (BottomRight.transform.position.x - UpperLeft.transform.position.x) * 0.5f;
        float z = BottomRight.transform.position.z - (BottomRight.transform.position.z - UpperLeft.transform.position.z) * 0.5f;

        worldMapCamera.transform.position = new Vector3(x, worldMapCamera.transform.position.y, z);
    }

    private void Init()
    {
        if (MapObjectList != null)
            return;

        MapObjectList = new();

        worldMapCamera = GetComponentInChildren<WorldMapCamera>();
        worldMapCamera.SetWorldMap(this);
    }

    /// <summary>
    /// World Map Width Size / MapUI Width Size
    /// </summary>
    /// <returns></returns>
    public float GetWorldMapWidthRatio(float MapWidth)
    {
        return GetWorldMapWidth() / MapWidth;
    }

    public float GetWorldMapWidth()
    {
        return Mathf.Abs(BottomRight.transform.position.x - UpperLeft.transform.position.x);
    }

    public float GetWorldMapHeight()
    {
        return Mathf.Abs(BottomRight.transform.position.z - UpperLeft.transform.position.z);
    }

    /// <summary>
    /// World Map Height Size / MapUI Height Size
    /// </summary>
    /// <returns></returns>
    public float GetWorldMapHeightRatio(float MapHeight)
    {
        return GetWorldMapHeight() / MapHeight;
    }

    public Vector3 GetMapLocation(Transform transform)
    {
        Vector3 position = transform.position;

        position.x -= UpperLeft.transform.position.x;
        position.z -= BottomRight.transform.position.z;

        return position;
    }

    /// <summary>
    ///  Get World position from Map UI position
    /// </summary>
    public Vector3 GetWorldMapLocation(Vector2 mapSize, Vector2 MapScreenPosition)
    {
        float x = UpperLeft.transform.position.x + MapScreenPosition.x * GetWorldMapWidthRatio(mapSize.x);
        float z = BottomRight.transform.position.z + MapScreenPosition.y * GetWorldMapHeightRatio(mapSize.y);

        return new Vector3(x, 0f, z);
    }

    public void CallOnMapObjectAdd(MapIconData MapIconData)
    {
        Init();

        if (GetMapIconData(MapIconData.mapObject) != null)
            return;

        MapObjectList.Add(MapIconData.mapObject, MapIconData);

        OnMapObjectAdd?.Invoke(MapIconData.mapObject);
    }

    public MapIconData GetMapIconData(MapObject mapObject)
    {
        if (mapObject == null || !MapObjectList.ContainsKey(mapObject))
            return null;

        return MapObjectList[mapObject];
    }

    public void CallOnMapObjectRemove(MapIconData MapIconData)
    {
        if (MapIconData == null || !MapObjectList.Remove(MapIconData.mapObject))
            return;

        OnMapObjectRemove?.Invoke(MapIconData.mapObject);
    }

    // Start is called before the first frame update
    private void OnDestroy()
    {
    }
}
