using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportWaypoint : InteractiveMapObject, IInteractable
{
    public void Interact(Transform sourceTransform)
    {
    }

    public override MapIconAction GetMapIconActionComponent(MapIcon MapIcon)
    {
        return new TeleportWaypointMapIconAction(MapIcon);
    }

    protected override MapIconData CreateMapIconData()
    {
        return new TeleportWaypointData(this);
    }
}
