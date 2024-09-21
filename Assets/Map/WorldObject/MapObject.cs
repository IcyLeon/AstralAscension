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

    public virtual MapIconTypeSO GetMapIconTypeSO()
    {
        return null;
    }

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

    public virtual void Action(Player player)
    {
    }

    public virtual string GetActionText()
    {
        return "";
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
