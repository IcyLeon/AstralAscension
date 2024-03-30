using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public PlayerInputSystem playerInputSystem { get; private set; }

    // Start is called before the first frame update
    private void Awake()
    {
        playerInputSystem = new PlayerInputSystem();
    }

    private void Start()
    {
        playerInputSystem.Player.ReviewCursor.performed += ReviewCursor_performed;
        playerInputSystem.Player.ReviewCursor.canceled += ReviewCursor_canceled;
        ToggleCursor(false);
    }

    private void ReviewCursor_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        ToggleCursor(false);
    }
    private void ReviewCursor_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        ToggleCursor(true);
    }

    private void ToggleCursor(bool val)
    {
        Cursor.visible = val;
        if (!val)
        {
            Cursor.lockState = CursorLockMode.Locked;
            return;
        }
        Cursor.lockState = CursorLockMode.None;
    }
    private void OnDestroy()
    {
        playerInputSystem.Player.ReviewCursor.performed -= ReviewCursor_performed;
        playerInputSystem.Player.ReviewCursor.canceled -= ReviewCursor_canceled;
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
