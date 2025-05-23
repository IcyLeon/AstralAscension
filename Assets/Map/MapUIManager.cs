using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUIManager : MonoBehaviour
{
    private MapUI[] MapUIList;
    private UIManager uiManager;
    private PlayerManager playerManager;

    private void Awake()
    {
        MapUIList = GetComponentsInChildren<MapUI>(true);
        uiManager = GetComponentInParent<UIManager>();
        uiManager.OnPlayerManagerChanged += UiManager_OnPlayerManagerChanged;
    }

    private void UiManager_OnPlayerManagerChanged()
    {
        playerManager = uiManager.playerManager;

        foreach (var MapUI in MapUIList)
        {
            MapUI.SetPlayer(playerManager.player);
            MapUI.Init();
        }
    }

    private void OnDestroy()
    {
        uiManager.OnPlayerManagerChanged -= UiManager_OnPlayerManagerChanged;
    }
}