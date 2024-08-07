using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WorldMapManager;

public class PlayerIcon : MonoBehaviour, IMapIconWidget
{
    [SerializeField] private MapIconTypeSO m_MapIconType;

    public void Spawn()
    {
        if (instance == null)
        {
            Debug.LogError("World Map Manager not found!");
            return;
        }

        instance.CallOnMapObjectAdd(this);
    }

    public MapIconAction AddMapIconActionComponent(MapIcon MapIcon)
    {
        return new PlayerMapIconAction(MapIcon);
    }

    public MapIconTypeSO GetMapIconTypeSO()
    {
        return m_MapIconType;
    }

    public Transform GetMapIconTransform()
    {
        return transform;
    }

    void Start()
    {
        Spawn();
    }
}
