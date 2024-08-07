using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInputSystem playerInputSystem;

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

    // Start is called before the first frame update
    private void Awake()
    {
        playerInputSystem = new PlayerInputSystem();
    }

    private void Start()
    {
        uiInputAction.ReviewCursor.performed += ReviewCursor_performed;
        uiInputAction.ReviewCursor.canceled += ReviewCursor_canceled;
        //ToggleCursor(false);
    }

    private void ReviewCursor_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        //ToggleCursor(false);
    }
    private void ReviewCursor_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        //ToggleCursor(true);
    }

    public void ToggleCursor(bool val)
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
        uiInputAction.ReviewCursor.performed -= ReviewCursor_performed;
        uiInputAction.ReviewCursor.canceled -= ReviewCursor_canceled;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
