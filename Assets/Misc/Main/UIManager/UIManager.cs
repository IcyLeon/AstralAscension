using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerManager playerManager { get; private set; }
    public event Action OnPlayerManagerChanged;

    private void Start()
    {
        playerManager = PlayerManager.instance;
        OnPlayerManagerChanged?.Invoke();
    }
}
