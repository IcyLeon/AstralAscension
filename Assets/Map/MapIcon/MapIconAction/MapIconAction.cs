using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapIconAction
{
    protected MapIcon mapIcon { get; private set; }

    public MapIconAction(MapIcon MapIcon)
    {
        mapIcon = MapIcon;
        Init();
    }

    public void Init()
    {
        MapIconTypeSO mapIconTypeSO = mapIcon.mapObject.GetMapIconTypeSO();

        if (mapIconData == null || mapIconTypeSO == null)
            return;

        mapIconData.SetMapIconTypeSO(mapIconTypeSO);
    }

    public MapIconData mapIconData
    {
        get
        {
            return mapIcon.mapObject.mapIconData;
        }
    }

    public abstract bool ShowActionOption();
    public abstract string GetActionText();

    public abstract void Action();

    public virtual void Update()
    {

    }

    public virtual void OnDestroy()
    {
    }
}
