using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public PlayerInputSystem playerInputSystem { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        playerInputSystem = new PlayerInputSystem();
    }

    private void OnEnable()
    {
        playerInputSystem.Enable();
    }

    private void OnDisable()
    {
        playerInputSystem.Disable();
    }
}
