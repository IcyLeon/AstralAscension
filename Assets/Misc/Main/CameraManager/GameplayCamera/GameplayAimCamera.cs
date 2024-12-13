using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayAimCamera : GameplayCamera
{
    private float aimTargetYaw;
    private float aimTargetPitch;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        aimTargetYaw = aimTargetPitch = 0f;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        aimTargetYaw = GetAngle(cameraManager.CameraMain.transform.eulerAngles.y);
        aimTargetPitch = GetAngle(cameraManager.CameraMain.transform.eulerAngles.x);
        Update3rdPersonCam();
        cameraManager.playerController.playerInputAction.Look.performed += Look_performed;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        cameraManager.playerController.playerInputAction.Look.performed -= Look_performed;
    }

    private void Look_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Vector2 look = obj.ReadValue<Vector2>();
        aimTargetPitch += (look.y * -1f) * Time.deltaTime * cameraManager.CameraSO.CameraAimData.CameraRotationSpeed;
        aimTargetYaw += look.x * Time.deltaTime * cameraManager.CameraSO.CameraAimData.CameraRotationSpeed;

        aimTargetYaw = GetAngle(aimTargetYaw);
        aimTargetPitch = Mathf.Clamp(aimTargetPitch, -89f, 89f);
    }

    private float GetAngle(float angle)
    {
        float val = angle;
        if (val > 180f)
            val -= 360f;
        if (val < -180f)
            val += 360f;

        return val;
    }

    private void Update3rdPersonCam()
    {
        cameraManager.CameraTarget.transform.rotation = Quaternion.Euler(aimTargetPitch, aimTargetYaw, 0f);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Update3rdPersonCam();
    }
}
