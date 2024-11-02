using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CameraSelectionPan
{
    protected CameraSelectionPanStorage cameraSelectionPanStorage;
    public CameraSelectionPan(CameraSelectionPanStorage CameraSelectionPanStorage)
    {
        cameraSelectionPanStorage = CameraSelectionPanStorage;
    }

    public virtual void Enter()
    {
        cameraSelectionPanStorage.cameraPanData.cameraRotationSmoothingSpeed = cameraSelectionPanStorage.cameraPanData.freeLookCameraPanData.BaseCameraRotationSmoothingSpeed;
    }

    public virtual void Exit()
    {

    }


    protected void InitBaseData()
    {
        cameraSelectionPanStorage.cameraPanData.targetCameraDistance = cameraSelectionPanStorage.cameraPanData.freeLookCameraPanData.BaseCameraDistance;
        cameraSelectionPanStorage.cameraPanData.cameraRotationSpeed = cameraSelectionPanStorage.cameraPanData.freeLookCameraPanData.BaseCameraRotationSpeed;
    }

    public virtual void Update()
    {
        cameraSelectionPanStorage.cameraPanData.cinemachine3rdPersonFollow.CameraDistance = Mathf.SmoothStep(cameraSelectionPanStorage.cameraPanData.cinemachine3rdPersonFollow.CameraDistance, cameraSelectionPanStorage.cameraPanData.targetCameraDistance, Time.deltaTime * cameraSelectionPanStorage.cameraPanData.freeLookCameraPanData.BaseCameraZoomSmoothingSpeed);

        UpdateRotation();
    }

    private void UpdateRotation()
    {
        CinemachineVirtualCamera virtualCamera = cameraSelectionPanStorage.cameraPanData.cameraRotationAroundTarget.VirtualCamera;
        Quaternion rotation = Quaternion.Euler(cameraSelectionPanStorage.cameraPanData.rotationValues.y, cameraSelectionPanStorage.cameraPanData.rotationValues.x, 0);
        virtualCamera.Follow.rotation = Quaternion.Lerp(virtualCamera.Follow.rotation, rotation, Time.deltaTime * cameraSelectionPanStorage.cameraPanData.cameraRotationSmoothingSpeed);
    }

    public virtual void OnDrag(Vector2 delta)
    {
        cameraSelectionPanStorage.cameraPanData.RotateAroundTarget(delta);
    }

    public virtual void OnScroll(Vector2 delta)
    {

    }
}
