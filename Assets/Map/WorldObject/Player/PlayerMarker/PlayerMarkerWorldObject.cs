using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WorldMapManager;

public class PlayerMarkerWorldObject : MonoBehaviour, IMapIconWidget
{
    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        if (instance == null)
        {
            Debug.LogError("World Map Manager not found!");
            return;
        }

        instance.CallOnMapObjectAdd(this);
    }

    public MapIconAction AddMapIconActionComponent(MapIcon MapIcon)
    {
        return new PlayerMarkerMapIconAction(MapIcon);
    }

    public Transform GetMapIconTransform()
    {
        return transform;
    }

    public MapIconTypeSO GetMapIconTypeSO()
    {
        return null;
    }

    private void OnDestroy()
    {
        instance.CallOnMapObjectRemove(this);
    }
}
