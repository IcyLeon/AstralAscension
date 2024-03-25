using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerSO PlayerSO { get; private set; }
    [field: SerializeField] public Rigidbody Rb { get; private set; }
    [field: SerializeField] public CameraManager CameraManager { get; private set; }
    public PlayerData playerData { get; private set; }

    private PlayerInput playerInput;

    // Start is called before the first frame update
    private void Awake()
    {
        playerData = new PlayerData(this);
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
