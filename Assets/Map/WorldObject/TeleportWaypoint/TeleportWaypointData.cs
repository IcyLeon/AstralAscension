using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportWaypointData : MapIconData
{
    private bool lockStatus;

    public TeleportWaypointData(MapObject MapObject) : base(MapObject)
    {
        lockStatus = false;
    }
}
