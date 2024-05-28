using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAim : MonoBehaviour
{
    private CameraManager cameraManager;

    private float aimTargetYaw;
    private float aimTargetPitch;

    // Start is called before the first frame update
    private void Awake()
    {
        cameraManager = GetComponentInParent<CameraManager>();
        aimTargetYaw = aimTargetPitch = 0f;
    }

    private void OnEnable()
    {
        aimTargetYaw = GetAngle(cameraManager.CameraMain.transform.eulerAngles.y);
        aimTargetPitch = GetAngle(cameraManager.CameraMain.transform.eulerAngles.x);

        cameraManager.Player.PlayerController.playerInputAction.Look.performed += Look_performed;
    }
    private void OnDisable()
    {
        cameraManager.Player.PlayerController.playerInputAction.Look.performed -= Look_performed;
    }

    private void Look_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Vector2 look = obj.ReadValue<Vector2>();
        aimTargetPitch += (look.y * -1f) * Time.deltaTime * cameraManager.CameraSO.CameraAimData.RotationSpeed;
        aimTargetYaw += look.x * Time.deltaTime * cameraManager.CameraSO.CameraAimData.RotationSpeed;

        aimTargetYaw = GetAngle(aimTargetYaw);
        aimTargetPitch = Mathf.Clamp(aimTargetPitch, cameraManager.playerPOV.m_VerticalAxis.m_MinValue, cameraManager.playerPOV.m_VerticalAxis.m_MaxValue);
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
    private void Update()
    {
        Update3rdPersonCam();
    }
}
