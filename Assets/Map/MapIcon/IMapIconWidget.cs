using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMapIconWidget
{
    void Spawn();
    MapIconAction AddMapIconComponent(MapIcon MapIcon);
    MapIconTypeSO GetMapIconTypeSO();
    Transform GetMapIconTransform();
}
