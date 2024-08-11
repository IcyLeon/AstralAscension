using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WorldMapManager;

public class PlayerMarkerWorldObject : MapObject
{
    protected override MapIconData CreateMapIconData()
    {
        return new MapIconData(this);
    }

    public override MapIconAction GetMapIconActionComponent(MapIcon MapIcon)
    {
        return new PlayerMarkerMapIconAction(MapIcon);
    }

    public override MapIconTypeSO GetMapIconTypeSO()
    {
        return null;
    }

    private void OnDestroy()
    {
        instance.CallOnMapObjectRemove(this);
    }
}
