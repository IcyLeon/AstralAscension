using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportWaypointMapIconAction : InteractiveMapIconAction
{
    public TeleportWaypointMapIconAction(MapIcon mapIcon) : base(mapIcon)
    {
    }

    public override bool ShowActionOption()
    {
        return true;
    }

    public override string GetActionText()
    {
        return "Teleport";
    }

    public override void Action()
    {
        Player player = mapIcon.worldMapBackground.player;

        player.transform.position = mapIcon.mapObject.GetMapIconTransform().position;
    }
}
