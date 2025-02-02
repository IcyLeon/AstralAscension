using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    private static PlayerController playerControllerInstance;
    private PlayerInputSystem playerInputSystem;

    public static PlayerController instance
    {
        get
        {
            return GetInstance();
        }
    }

    public PlayerInputSystem.PlayerActions playerInputAction
    {
        get
        {
            return playerInputSystem.Player;
        }
    }

    public PlayerInputSystem.UIActions uiInputAction
    {
        get
        {
            return playerInputSystem.UI;
        }
    }

    public PlayerInputSystem.MapActions mapInputAction
    {
        get
        {
            return playerInputSystem.Map;
        }
    }

    public PlayerInputSystem.CharacterDisplayActions characterDisplayInputAction
    {
        get
        {
            return playerInputSystem.CharacterDisplay;
        }
    }

    // Start is called before the first frame update
    private PlayerController()
    {
        CreateInputSystem();
        OnEnable();
    }

    private void OnEnable()
    {
        playerInputSystem.Enable();
        uiInputAction.Enable();
    }

    private void OnDisable()
    {
        playerInputSystem.Disable();
        uiInputAction.Disable();
    }

    private void CreateInputSystem()
    {
        playerInputSystem = new PlayerInputSystem();
    }

    private static PlayerController GetInstance()
    {
        if (playerControllerInstance == null)
        {
            playerControllerInstance = new PlayerController();
        }

        return playerControllerInstance;
    }
}

