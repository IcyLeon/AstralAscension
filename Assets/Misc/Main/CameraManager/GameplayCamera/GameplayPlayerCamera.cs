using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayPlayerCamera : GameplayCamera
{
    private CinemachineFramingTransposer playerTransposerCameras;
    private float m_TargetZoom;

    protected override void Start()
    {
        base.Start();
        m_TargetZoom = cameraManager.CameraSO.CameraDefaultData.CameraDistanceRange.y;
        playerTransposerCameras = VirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        cameraManager.playerController.playerInputAction.Zoom.performed += Zoom_performed;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        cameraManager.playerController.playerInputAction.Zoom.performed -= Zoom_performed;
    }

    private void Zoom_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        m_TargetZoom += cameraManager.playerController.playerInputAction.Zoom.ReadValue<Vector2>().y;
        m_TargetZoom = Mathf.Clamp(m_TargetZoom, cameraManager.CameraSO.CameraDefaultData.CameraDistanceRange.x, cameraManager.CameraSO.CameraDefaultData.CameraDistanceRange.y);
    }

    private void UpdateZoomCamera()
    {
        playerTransposerCameras.m_CameraDistance = Mathf.SmoothStep(playerTransposerCameras.m_CameraDistance, m_TargetZoom, Time.deltaTime * cameraManager.CameraSO.CameraDefaultData.CameraZoomSmoothingSpeed);
    }

    protected override void Update()
    {
        base.Update();
        UpdateZoomCamera();
    }
}
