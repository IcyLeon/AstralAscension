using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    private PlayerInputSystem playerInputSystem;

    public PlayerInputSystem.PlayerActions playerInputAction
    {
        get
        {
            return playerInputSystem.Player;
        }
    }

    // Start is called before the first frame update
    public PlayerController()
    {
        CreateInputSystem();
    }

    public void OnEnable()
    {
        playerInputSystem.Enable();
    }

    public void OnDisable()
    {
        playerInputSystem.Disable();
    }

    private void CreateInputSystem()
    {
        playerInputSystem = new PlayerInputSystem();
    }

}

