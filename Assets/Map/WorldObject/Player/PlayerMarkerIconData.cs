using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMarkerIconData : MapIconData
{
    private bool confirmPlacementStatus;

    public PlayerMarkerIconData(MapObject MapObject) : base(MapObject)
    {
        confirmPlacementStatus = false;
    }

    public override bool IsConfirmedPlaced()
    {
        return confirmPlacementStatus;
    }

    public void ConfirmPlacement()
    {
        confirmPlacementStatus = true;
        CallMapIconChanged();
    }
}
