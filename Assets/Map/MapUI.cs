using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public abstract class MapUI : MonoBehaviour
{
    public WorldMapBackground worldMapBackground { get; protected set; }

    protected virtual void Awake()
    {
        worldMapBackground = GetComponentInChildren<WorldMapBackground>();
        worldMapBackground.OnMapIconAdd += OnMapIconAdd;
        worldMapBackground.OnMapIconRemove += OnMapIconRemove;
    }

    protected virtual void OnMapIconAdd(MapIcon mapIcon)
    {

    }
    protected virtual void OnMapIconRemove(MapIcon mapIcon)
    {

    }

    protected virtual void OnDestroy()
    {
        if (worldMapBackground == null)
            return;

        worldMapBackground.OnMapIconAdd -= OnMapIconAdd;
        worldMapBackground.OnMapIconRemove -= OnMapIconRemove;
    }
}
