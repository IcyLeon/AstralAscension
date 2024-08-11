using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapIconData
{
    private MapIconTypeSO mapIconTypeSO;
    public MapObject mapObject { get; private set; }

    public event EventHandler OnMapIconChanged;
    public MapIconData(MapObject MapObject)
    {
        mapObject = MapObject;
    }

    public MapIconTypeSO GetMapIconTypeSO()
    {
        return mapIconTypeSO;
    }

    public void SetMapIconTypeSO(MapIconTypeSO MapIconTypeSO)
    {
        mapIconTypeSO = MapIconTypeSO;
        OnMapIconChanged?.Invoke(this, EventArgs.Empty);
    }
}
