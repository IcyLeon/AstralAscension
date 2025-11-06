using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUIManager : MonoBehaviour
{
    private MapUI[] MapUIList;
    private void Awake()
    {
        MapUIList = GetComponentsInChildren<MapUI>(true);
        PlayerManager.OnPlayerChanged += PlayerManager_OnPlayerChanged;
    }

    private void PlayerManager_OnPlayerChanged(Player player)
    {
        foreach (var MapUI in MapUIList)
        {
            MapUI.SetPlayer(player);
            MapUI.Init();
        }
    }


    private void OnDestroy()
    {
        PlayerManager.OnPlayerChanged -= PlayerManager_OnPlayerChanged;
    }
}