using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapIconData
{
    public string mapIconName { get; private set; }
    public Sprite mapIconSprite { get; private set; }
    public string mapIconDescription { get; private set; }

    public MapObject mapObject { get; private set; }

    public event EventHandler OnMapIconChanged;
    public MapIconData(MapObject MapObject)
    {
        mapObject = MapObject;
    }

    public virtual bool IsConfirmedPlaced()
    {
        return true;
    }

    public void SetMarkerName(string name)
    {
        mapIconName = name;
        CallMapIconChanged();
    }
    public void SetMarkerSprite(Sprite sprite)
    {
        mapIconSprite = sprite;
        CallMapIconChanged();
    }

    public void SetMarkerDesc(string desc)
    {
        mapIconDescription = desc;
        CallMapIconChanged();
    }

    protected void CallMapIconChanged()
    {
        OnMapIconChanged?.Invoke(this, EventArgs.Empty);
    }

    public virtual void OnDestroy()
    {

    }
}
