using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorVisibility : MonoBehaviour
{
    private PlayerController playerController;

    private void Awake()
    {
        playerController = PlayerController.instance;
        playerController.uiInputAction.ReviewCursor.performed += ReviewCursor_performed;
        playerController.uiInputAction.ReviewCursor.canceled += ReviewCursor_canceled;
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
        playerController.uiInputAction.ReviewCursor.performed -= ReviewCursor_performed;
        playerController.uiInputAction.ReviewCursor.canceled -= ReviewCursor_canceled;
    }
}
