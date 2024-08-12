using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUIManager : MonoBehaviour
{
    private MapUI[] MapUIList;

    private void Awake()
    {
        MapUIList = GetComponentsInChildren<MapUI>(true);

        foreach(var MapUI in MapUIList)
        {
            MapUI.Init();
        }
    }
}
