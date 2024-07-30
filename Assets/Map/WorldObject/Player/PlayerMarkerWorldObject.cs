using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WorldMapManager;

public class PlayerMarkerWorldObject : MonoBehaviour, IMapIconWidget
{
    [SerializeField] private MapIconTypeSO testSO;


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

        instance.CallOnMapIconAdd(this);
    }

    public MapIconAction AddMapIconComponent(MapIcon MapIcon)
    {
        return new InteractiveMapIconAction(MapIcon);
    }

    public Transform GetMapIconTransform()
    {
        return transform;
    }

    public MapIconTypeSO GetMapIconTypeSO()
    {
        return testSO;
    }

    private void OnDestroy()
    {
        instance.CallOnMapIconRemove(this);
    }
}
