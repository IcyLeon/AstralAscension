using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMapIconWidget
{
    void Spawn();
    MapIconAction AddMapIconActionComponent(MapIcon MapIcon);
    MapIconTypeSO GetMapIconTypeSO();
    Transform GetMapIconTransform();
}
