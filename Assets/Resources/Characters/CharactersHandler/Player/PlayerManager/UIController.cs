using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController
{
    private static UIController uiControllerInstance;
    private UIInputSystem uiInputSystem;

    public static UIController instance
    {
        get
        {
            return GetInstance();
        }
    }


    public UIInputSystem.UIActions uiInputAction
    {
        get
        {
            return uiInputSystem.UI;
        }
    }

    public UIInputSystem.MapActions mapInputAction
    {
        get
        {
            return uiInputSystem.Map;
        }
    }

    public UIInputSystem.WorldActions worldInputAction
    {
        get
        {
            return uiInputSystem.World;
        }
    }

    public UIInputSystem.CharacterDisplayActions characterDisplayInputAction
    {
        get
        {
            return uiInputSystem.CharacterDisplay;
        }
    }

    // Start is called before the first frame update
    private UIController()
    {
        CreateInputSystem();
        OnEnable();
    }

    private void OnEnable()
    {
        uiInputSystem.Enable();
        uiInputAction.Enable();
    }

    private void OnDisable()
    {
        uiInputSystem.Disable();
        uiInputAction.Disable();
    }

    private void CreateInputSystem()
    {
        uiInputSystem = new UIInputSystem();
    }

    private static UIController GetInstance()
    {
        if (uiControllerInstance == null)
        {
            uiControllerInstance = new UIController();
        }

        return uiControllerInstance;
    }
}

