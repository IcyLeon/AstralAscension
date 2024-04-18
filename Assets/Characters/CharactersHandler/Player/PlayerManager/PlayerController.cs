using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Player Player;
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

    // Start is called before the first frame update
    private void Awake()
    {
        playerInputSystem = new PlayerInputSystem();
    }

    private void Start()
    {
        playerInputAction.SwitchCharacters.performed += SwitchCharacters_performed;
        uiInputAction.ReviewCursor.performed += ReviewCursor_performed;
        uiInputAction.ReviewCursor.canceled += ReviewCursor_canceled;
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

    private void SwitchCharacters_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        int numKeyValue;
        int.TryParse(obj.control.name, out numKeyValue);
    }

    private void OnDestroy()
    {
        playerInputAction.SwitchCharacters.performed -= SwitchCharacters_performed;
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
