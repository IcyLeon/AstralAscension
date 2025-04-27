using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorVisibility : MonoBehaviour
{
    private UIController uiController;

    private void Awake()
    {
        uiController = UIController.instance;
        uiController.uiInputAction.ReviewCursor.performed += ReviewCursor_performed;
        uiController.uiInputAction.ReviewCursor.canceled += ReviewCursor_performed;
    }
    private void ReviewCursor_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        ToggleCursor(obj.ReadValueAsButton());
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
        uiController.uiInputAction.ReviewCursor.performed -= ReviewCursor_performed;
        uiController.uiInputAction.ReviewCursor.canceled -= ReviewCursor_performed;
    }
}
