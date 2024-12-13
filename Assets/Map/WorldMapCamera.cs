using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WorldMapCamera : MonoBehaviour
{
    private Camera Camera;
    private WorldMapManager worldMap;
    private CommandBuffer commandBuffer;

    private void Awake()
    {
        Camera = GetComponent<Camera>();
    }

    public void SetWorldMap(WorldMapManager WorldMapManager)
    {
        worldMap = WorldMapManager;
        CalculateWorldBounds();
    }

    private void CalculateWorldBounds()
    {
        if (Camera == null)
            return;

        float aspectRatio = GetHighestLength() / GetLowestLength();
        Camera.orthographicSize = GetLowestLength() / 2;
        Camera.aspect = aspectRatio;
    }

    private float GetLowestLength()
    {
        float x = worldMap.GetWorldMapWidth();
        float y = worldMap.GetWorldMapHeight();
        if (x > y)
            return y;
        else
            return x;
    }

    private float GetHighestLength()
    {
        float x = worldMap.GetWorldMapWidth();
        float y = worldMap.GetWorldMapHeight();
        if (x > y)
            return x;
        else
            return y;
    }

}
