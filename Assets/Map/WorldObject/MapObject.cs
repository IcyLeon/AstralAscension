using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WorldMapManager;

public abstract class MapObject : MonoBehaviour
{
    public event EventHandler OnMapObjectChanged;
    public MapIconData mapIconData { get; private set; }
    protected abstract MapIconData CreateMapIconData();
    public abstract MapIconAction GetMapIconActionComponent(MapIcon MapIcon);
    public abstract MapIconTypeSO GetMapIconTypeSO();

    public Transform GetMapIconTransform()
    {
        return transform;
    }

    public void DestroyMapObject()
    {
        if (gameObject == null)
            return;

        Destroy(gameObject);
    }

    private void Start()
    {
        if (instance == null)
        {
            Debug.LogError("World Map Manager not found!");
            return;
        }
        mapIconData = CreateMapIconData();

        instance.CallOnMapObjectAdd(mapIconData);
    }
}
