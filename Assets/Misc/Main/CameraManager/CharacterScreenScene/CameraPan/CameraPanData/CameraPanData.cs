using Cinemachine;
using UnityEngine;

public class CameraPanData
{
    public CameraRotationAroundTarget cameraRotationAroundTarget { get; private set; }
    public Cinemachine3rdPersonFollow cinemachine3rdPersonFollow;
    public FreeLookCameraPanData freeLookCameraPanData { get; private set; }
    public CloseUpCameraPanData closeUpCameraPanData { get; private set; }

    public float cameraZoomSpeed;
    public float targetCameraDistance;
    public float cameraRotationSpeed;
    public float cameraRotationSmoothingSpeed;
    public Vector2 rotationValues;

    public CameraPanData(CameraRotationAroundTarget CameraRotationAroundTarget)
    {
        cameraRotationAroundTarget = CameraRotationAroundTarget;
        ResetRotation();
        cinemachine3rdPersonFollow = cameraRotationAroundTarget.VirtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        cameraZoomSpeed = 0f;
        targetCameraDistance = 0f;
        cameraRotationSpeed = 0f;
        cameraRotationSmoothingSpeed = 0f;
        freeLookCameraPanData = cameraRotationAroundTarget.CameraPanSelectionDataSO.FreeLookCameraPanData;
        closeUpCameraPanData = cameraRotationAroundTarget.CameraPanSelectionDataSO.CloseUpCameraPanData;
    }

    public void RotateAroundTarget(Vector2 AddRotationVector)
    {
        rotationValues.x += Time.deltaTime * cameraRotationSpeed * AddRotationVector.x;
        rotationValues.y += Time.deltaTime * cameraRotationSpeed * AddRotationVector.y * -1f;

        ClampRotation();
    }

    public void ResetRotation()
    {
        rotationValues = Vector2.zero;
    }

    private void ClampRotation()
    {
        if (rotationValues.x >= 180f)
            rotationValues.x -= 360f;
        if (rotationValues.x <= -180f)
            rotationValues.x -= 360f;

        rotationValues.y = Mathf.Clamp(rotationValues.y, -65f, 65f);
    }
}
