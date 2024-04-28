using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Transform CameraTarget;
    [SerializeField] CameraSO CameraSO;

    [Header("Cinemachine Camera Components")]
    [SerializeField] CinemachineVirtualCamera PlayerCamera;
    [SerializeField] CinemachineVirtualCamera AimCamera;
    private CinemachineFramingTransposer playerTransposerCameras;
    private CinemachinePOV playerPOV;

    private float aimTargetYaw;
    private float aimTargetPitch;

    private float m_TargetZoom;
    public Camera CameraMain { get; private set; }
    private Player player;


    private Coroutine toggleAimCameraCoroutine;

    private void Awake()
    {
        aimTargetYaw = aimTargetPitch = 0f;

        PlayerCamera.Follow = CameraTarget;
        AimCamera.Follow = CameraTarget;

        player = transform.parent.GetComponent<Player>();
        playerTransposerCameras = PlayerCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        playerPOV = PlayerCamera.GetCinemachineComponent<CinemachinePOV>();
    }

    private IEnumerator ToggleAimCameraDelayCoroutine(bool enable, float time)
    {
        yield return new WaitForSeconds(time);
        EnableAimCamera(enable);
        toggleAimCameraCoroutine = null;
    }

    public void ToggleAimCamera(bool enable, float time = 0f)
    {
        if (time == 0f)
        {
            EnableAimCamera(enable);
            return;
        }

        if (toggleAimCameraCoroutine != null)
            StopCoroutine(toggleAimCameraCoroutine);

        toggleAimCameraCoroutine = StartCoroutine(ToggleAimCameraDelayCoroutine(enable, time));
    }

    private float GetAngle(float angle)
    {
        float val = angle;
        if (val > 180f)
            val -= 360f;

        return val;
    }

    private void EnableAimCamera(bool enable)
    {
        if (enable)
        {
            aimTargetPitch = GetAngle(CameraMain.transform.rotation.eulerAngles.x);
            aimTargetYaw = GetAngle(CameraMain.transform.rotation.eulerAngles.y);
        }

        AimCamera.gameObject.SetActive(enable);
    }

    public bool IsAimCameraActive()
    {
        return AimCamera.gameObject.activeSelf;
    }

    private void Start()
    {
        m_TargetZoom = CameraSO.maxZoom;
        transform.SetParent(null);
        CameraMain = Camera.main;

        player.PlayerController.playerInputAction.Look.performed += Look_performed;
        player.PlayerController.playerInputAction.Zoom.performed += Zoom_performed;
    }

    private void Update3rdPersonCam()
    {
        if (aimTargetYaw > playerPOV.m_HorizontalAxis.m_MaxValue)
        {
            aimTargetYaw -= playerPOV.m_HorizontalAxis.m_MaxValue - playerPOV.m_HorizontalAxis.m_MinValue;
        }
        else if (aimTargetYaw < playerPOV.m_HorizontalAxis.m_MinValue)
        {
            aimTargetYaw += playerPOV.m_HorizontalAxis.m_MaxValue - playerPOV.m_HorizontalAxis.m_MinValue;
        }

        aimTargetPitch = Mathf.Clamp(aimTargetPitch, playerPOV.m_VerticalAxis.m_MinValue, playerPOV.m_VerticalAxis.m_MaxValue);
        
        CameraTarget.transform.rotation = Quaternion.Euler(aimTargetPitch, aimTargetYaw, 0f);
    }

    private void Look_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!IsAimCameraActive())
            return;

        Vector2 look = player.PlayerController.playerInputAction.Look.ReadValue<Vector2>();
        aimTargetPitch += (look.y * -1f) * Time.deltaTime * CameraSO.CameraAimData.RotationSpeed;
        aimTargetYaw += look.x * Time.deltaTime * CameraSO.CameraAimData.RotationSpeed;
    }

    private void OnDestroy()
    {
        player.PlayerController.playerInputAction.Look.performed -= Look_performed;
        player.PlayerController.playerInputAction.Zoom.performed -= Zoom_performed;
    }

    private void UpdateZoomCamera()
    {
        m_TargetZoom = Mathf.Clamp(m_TargetZoom, CameraSO.minZoom, CameraSO.maxZoom);
        playerTransposerCameras.m_CameraDistance = Mathf.Lerp(playerTransposerCameras.m_CameraDistance, m_TargetZoom, Time.deltaTime * CameraSO.zoomSoothing);
    }

    private void Zoom_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        m_TargetZoom += player.PlayerController.playerInputAction.Zoom.ReadValue<Vector2>().y;
    }

    // Update is called once per frame
    private void Update()
    {
        Update3rdPersonCam();
        UpdateZoomCamera();
    }
}
