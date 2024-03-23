using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInput playerInput;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public PlayerInputSystem.PlayerActions playerInputAction
    {
        get
        {
            return playerInput.playerInputSystem.Player;
        }
    }
}
