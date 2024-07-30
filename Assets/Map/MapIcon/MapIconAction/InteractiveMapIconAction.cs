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

    public override void OnDestroy()
    {
    }

    public override void Update()
    {
        base.Update();

    }
}
