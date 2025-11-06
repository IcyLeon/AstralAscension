using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class UICanvas : MonoBehaviour
{

    // Start is called before the first frame update
    private void Start()
    {
    }

    public static bool IsValid()
    {
        InputSystemUIInputModule s_Module = (InputSystemUIInputModule)EventSystem.current.currentInputModule;
        return !s_Module.GetLastRaycastResult(Pointer.current.deviceId).isValid;
    }
}
