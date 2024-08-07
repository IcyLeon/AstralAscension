using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InteractiveMapIconAction : MapIconAction
{
    public InteractiveMapIconAction(MapIcon mapIcon) : base(mapIcon)
    {
    }
    public override bool ShowActionOption()
    {
        return true;
    }
    public override void OnDestroy()
    {
    }

    public override void Action()
    {
    }

    public override void Update()
    {
        base.Update();

    }

    public override string GetActionText()
    {
        return "";
    }
}
