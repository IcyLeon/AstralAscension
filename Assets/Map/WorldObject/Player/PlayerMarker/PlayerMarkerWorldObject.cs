using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WorldMapManager;

[DisallowMultipleComponent]
public class PlayerMarkerWorldObject : MapObject
{
    protected override MapIconData CreateMapIconData()
    {
        return new PlayerMarkerIconData(this);
    }

    private void OnDestroy()
    {
        instance.CallOnMapObjectRemove(mapIconData);
    }
}
