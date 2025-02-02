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
        m_TargetZoom = playerCameraManager.CameraSO.CameraDefaultData.CameraDistanceRange.y;
        playerTransposerCameras = VirtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        playerCameraManager.playerController.playerInputAction.Zoom.performed += Zoom_performed;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        playerCameraManager.playerController.playerInputAction.Zoom.performed -= Zoom_performed;
    }

    private void Zoom_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        m_TargetZoom += playerCameraManager.playerController.playerInputAction.Zoom.ReadValue<Vector2>().y;
        m_TargetZoom = Mathf.Clamp(m_TargetZoom, playerCameraManager.CameraSO.CameraDefaultData.CameraDistanceRange.x, playerCameraManager.CameraSO.CameraDefaultData.CameraDistanceRange.y);
    }

    private void UpdateZoomCamera()
    {
        playerTransposerCameras.m_CameraDistance = Mathf.SmoothStep(playerTransposerCameras.m_CameraDistance, m_TargetZoom, Time.deltaTime * playerCameraManager.CameraSO.CameraDefaultData.CameraZoomSmoothingSpeed);
    }

    protected override void Update()
    {
        base.Update();
        UpdateZoomCamera();
    }
}
