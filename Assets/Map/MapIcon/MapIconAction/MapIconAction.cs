using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapIconAction
{
    protected MapIcon mapIcon { get; private set; }

    public MapIconAction(MapIcon mapIcon)
    {
        this.mapIcon = mapIcon;
    }

    public virtual void Update()
    {

    }

    public virtual void OnDestroy()
    {
    }
}
