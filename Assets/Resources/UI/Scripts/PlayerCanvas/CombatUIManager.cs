using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUIManager : MonoBehaviour
{
    private PlayerManager playerManager;
    public Player player { get; private set; }
    public event Action OnPlayerChanged;

    private UIManager uiManager;

    // Start is called before the first frame update
    private void Awake()
    {
        uiManager = GetComponentInParent<UIManager>();
        uiManager.OnPlayerManagerChanged += UiManager_OnPlayerManagerChanged;
    }

    private void UiManager_OnPlayerManagerChanged()
    {
        playerManager = uiManager.playerManager;
        player = playerManager.player;
        OnPlayerChanged?.Invoke();
    }

    private void OnDestroy()
    {
        uiManager.OnPlayerManagerChanged -= UiManager_OnPlayerManagerChanged;
    }
}
