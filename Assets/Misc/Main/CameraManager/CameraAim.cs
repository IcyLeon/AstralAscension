using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAim : MonoBehaviour
{
    [SerializeField] private CameraManager CameraManager;

    private float aimTargetYaw;
    private float aimTargetPitch;

    // Start is called before the first frame update
    private void Awake()
    {
        aimTargetYaw = aimTargetPitch = 0f;
    }

    private void OnEnable()
    {
        aimTargetYaw = GetAngle(CameraManager.CameraMain.transform.eulerAngles.y);
        aimTargetPitch = GetAngle(CameraManager.CameraMain.transform.eulerAngles.x);

        CameraManager.Player.PlayerController.playerInputAction.Look.performed += Look_performed;
    }
    private void OnDisable()
    {
        CameraManager.Player.PlayerController.playerInputAction.Look.performed -= Look_performed;
    }

    private void Look_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Vector2 look = obj.ReadValue<Vector2>();
        aimTargetPitch += (look.y * -1f) * Time.deltaTime * CameraManager.CameraSO.CameraAimData.RotationSpeed;
        aimTargetYaw += look.x * Time.deltaTime * CameraManager.CameraSO.CameraAimData.RotationSpeed;

        aimTargetYaw = GetAngle(aimTargetYaw);

        aimTargetPitch = Mathf.Clamp(aimTargetPitch, CameraManager.playerPOV.m_VerticalAxis.m_MinValue, CameraManager.playerPOV.m_VerticalAxis.m_MaxValue);
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
        CameraManager.CameraTarget.transform.rotation = Quaternion.Euler(aimTargetPitch, aimTargetYaw, 0f);
    }

    // Update is called once per frame
    private void Update()
    {
        Update3rdPersonCam();
    }
}
