using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeLookCameraPanVirtualCamera : CameraPanVirtualCam
{
    private float targetCameraDistance;
    private float cameraZoomSpeed;
    private float cameraZoomSmoothingSpeed;
    private float originalCameraDistance;

    protected override void Awake()
    {
        base.Awake();

        originalCameraDistance = Cinemachine3rdPersonFollow.CameraDistance;
        cameraZoomSpeed = cameraPanManager.CameraPanSelectionDataSO.CameraZoomSpeed;
        cameraZoomSmoothingSpeed = cameraPanManager.CameraPanSelectionDataSO.CameraZoomSmoothingSpeed;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        targetCameraDistance = originalCameraDistance;
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            cameraPanManager.ChangeCamera(cameraPanManager.characterProfileCameraPanVirtualCamera);
            return;
        }

        Cinemachine3rdPersonFollow.CameraDistance = Mathf.SmoothStep(Cinemachine3rdPersonFollow.CameraDistance, targetCameraDistance, Time.unscaledDeltaTime * cameraZoomSmoothingSpeed);
    }

    public override void OnScroll(float delta)
    {
        base.OnScroll(delta);

        targetCameraDistance += Time.unscaledDeltaTime * cameraZoomSpeed * delta * -1f;
        targetCameraDistance = Mathf.Clamp(targetCameraDistance, cameraPanManager.CameraPanSelectionDataSO.CameraDistanceRange.x, cameraPanManager.CameraPanSelectionDataSO.CameraDistanceRange.y);
    }
}
