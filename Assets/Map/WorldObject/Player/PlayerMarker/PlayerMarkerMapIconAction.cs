using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMarkerMapIconAction : MapIconAction
{
    public PlayerMarkerMapIconAction(MapIcon mapIcon) : base(mapIcon)
    {
    }

    public override bool ShowActionOption()
    {
        return false;
    }

    public override string GetActionText()
    {
        return "";
    }

    public override void Action()
    {
    }
}
