using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class InteractiveMapObject : MapObject
{
    [SerializeField] private MapIconTypeSO m_MapIconTypeSO;

    public override MapIconTypeSO GetMapIconTypeSO()
    {
        return m_MapIconTypeSO;
    }

    public override MapIconAction GetMapIconActionComponent(MapIcon MapIcon)
    {
        return new InteractiveMapIconAction(MapIcon);
    }

    protected override MapIconData CreateMapIconData()
    {
        return new MapIconData(this);
    }
}