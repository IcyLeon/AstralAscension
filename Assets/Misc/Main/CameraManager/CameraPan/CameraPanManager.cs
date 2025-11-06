using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class CameraPanManager : MonoBehaviour
{
    [field: SerializeField] public CameraPanSelectionDataSO CameraPanSelectionDataSO { get; private set; }
    private CameraPanVirtualCam currentPanVirtualCam;
    private UIController uiController;

    private void Awake()
    {
        DisableAllVirtualCams();
    }

    private void DisableAllVirtualCams()
    {
        CameraPanVirtualCam[] CameraPanVirtualCamList = GetComponentsInChildren<CameraPanVirtualCam>(true);

        foreach (var Cam in CameraPanVirtualCamList)
        {
            Cam.OnExit();
        }
    }

    private void Start()
    {
        uiController = UIController.instance;
        uiController.characterDisplayInputAction.Zoom.performed += Zoom_performed;
        uiController.characterDisplayInputAction.Look.performed += Look_performed;
    }

    private void Look_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (currentPanVirtualCam == null || !UICanvas.IsValid())
            return;

        currentPanVirtualCam.OnDrag(obj.ReadValue<Vector2>());
    }

    public float GetCameraDistance()
    {
        if (currentPanVirtualCam == null)
            return CameraPanSelectionDataSO.CameraDistanceRange.x;

        return currentPanVirtualCam.GetCameraDistance();
    }

    private void Zoom_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (currentPanVirtualCam == null)
            return;

        currentPanVirtualCam.OnScroll(obj.ReadValue<Vector2>().y);
    }

    public void OnDrag(Vector2 delta)
    {
        if (currentPanVirtualCam == null)
            return;

        currentPanVirtualCam.OnDrag(delta);
    }

    public void OnScroll(float delta)
    {
        if (currentPanVirtualCam == null)
            return;

        currentPanVirtualCam.OnScroll(delta);
    }

    public void ChangeCamera(CameraPanVirtualCam CameraPanVirtualCam)
    {
        if (currentPanVirtualCam != null)
        {
            currentPanVirtualCam.OnExit();
        }

        currentPanVirtualCam = CameraPanVirtualCam;

        if (currentPanVirtualCam == null)
            return;

        currentPanVirtualCam.OnEnter();
    }
}
