using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportWaypoint : InteractiveMapObject, IInteractable
{
    public void Interact(Player Player)
    {
    }

    protected override MapIconData CreateMapIconData()
    {
        return new TeleportWaypointData(this);
    }

    public override string GetActionText()
    {
        return "Teleport";
    }

    public override void Action(Player player)
    {
        player.transform.position = GetMapIconTransform().position;
    }
}
