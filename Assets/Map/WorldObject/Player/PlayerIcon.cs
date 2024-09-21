using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIcon : MapObject
{
    [SerializeField] private MapIconTypeSO m_MapIconType;

    protected override MapIconData CreateMapIconData()
    {
        return new MapIconData(this);
    }

    public override MapIconTypeSO GetMapIconTypeSO()
    {
        return m_MapIconType;
    }
}
