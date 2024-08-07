using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapManager : MonoBehaviour
{
    public List<IMapIconWidget> IMapIconWidgetList { get; private set; }
    public static WorldMapManager instance { get; private set; }

    private Camera BirdEyeCamera;

    [SerializeField] private Transform UpperLeft;
    [SerializeField] private Transform BottomRight;

    public delegate void OnMapIconEvent(IMapIconWidget IMapIconWidget);
    public event OnMapIconEvent OnMapObjectAdd;
    public event OnMapIconEvent OnMapObjectRemove;

    private void Awake()
    {
        instance = this;
        Init();

        CenterCamera();
    }

    private void FindWorldMapCamera()
    {
        GameObject go = GameObject.FindGameObjectWithTag("WorldMapCamera");

        if (go == null)
            return;

        BirdEyeCamera = go.GetComponent<Camera>();
    }

    private void CenterCamera()
    {
        FindWorldMapCamera();

        if (BirdEyeCamera == null)
            return;

        float x = UpperLeft.transform.position.x + (BottomRight.transform.position.x - UpperLeft.transform.position.x) / 2f;
        float z = BottomRight.transform.position.z - (BottomRight.transform.position.z - UpperLeft.transform.position.z) / 2f;

        BirdEyeCamera.gameObject.transform.position = new Vector3(x, 999f, z);
    }

    private void Init()
    {
        if (IMapIconWidgetList != null)
            return;

        IMapIconWidgetList = new();
    }

    /// <summary>
    /// World Map Width Size / MapUI Width Size
    /// </summary>
    /// <returns></returns>
    public float GetWorldMapWidthRatio(float MapWidth)
    {
        float x = Mathf.Abs(BottomRight.transform.position.x - UpperLeft.transform.position.x);

        return x / MapWidth;
    }

    /// <summary>
    /// World Map Height Size / MapUI Height Size
    /// </summary>
    /// <returns></returns>
    public float GetWorldMapHeightRatio(float MapHeight)
    {
        float z = Mathf.Abs(BottomRight.transform.position.z - UpperLeft.transform.position.z);

        return z / MapHeight;
    }

    public Vector3 GetMapLocation(Transform transform)
    {
        Vector3 position = transform.position;

        position.x -= UpperLeft.transform.position.x;
        position.z -= BottomRight.transform.position.z;

        return position;
    }

    public Vector3 GetWorldMapLocation(Vector2 mapSize, Vector2 MapScreenPosition)
    {
        float x = UpperLeft.transform.position.x + MapScreenPosition.x * GetWorldMapWidthRatio(mapSize.x);
        float z = BottomRight.transform.position.z + MapScreenPosition.y * GetWorldMapHeightRatio(mapSize.y);

        return new Vector3(x, 0f, z);
    }

    public void CallOnMapObjectAdd(IMapIconWidget IMapIconWidget)
    {
        Init();

        if (IMapIconWidgetList.Contains(IMapIconWidget))
            return;

        IMapIconWidgetList.Add(IMapIconWidget);

        OnMapObjectAdd?.Invoke(IMapIconWidget);
    }

    public void CallOnMapObjectRemove(IMapIconWidget IMapIconWidget)
    {
        if (!IMapIconWidgetList.Remove(IMapIconWidget))
            return;

        OnMapObjectRemove?.Invoke(IMapIconWidget);
    }

    // Start is called before the first frame update
    private void OnDestroy()
    {
    }
}
