using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static WorldMapManager;

public abstract class InteractiveMapObject : MonoBehaviour, IMapIconWidget
{
    [SerializeField] private MapIconTypeSO m_MapIconTypeSO;
    public event EventHandler OnMapObjectChanged;

    private void Start()
    {
        Spawn(); 
    }

    public void Spawn()
    {
        if (instance == null)
        {
            Debug.LogError("World Map Manager not found!");
            return;
        }

        instance.CallOnMapObjectAdd(this);
    }


    public Transform GetMapIconTransform()
    {
        return transform;
    }

    protected void CallOnMapObjectChanged()
    {
        OnMapObjectChanged?.Invoke(this, EventArgs.Empty);
    }

    public MapIconTypeSO GetMapIconTypeSO()
    {
        return m_MapIconTypeSO;
    }

    public virtual MapIconAction AddMapIconActionComponent(MapIcon MapIcon)
    {
        return new InteractiveMapIconAction(MapIcon);
    }
}
