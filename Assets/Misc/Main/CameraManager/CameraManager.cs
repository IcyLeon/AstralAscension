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

    private void Awake()
    {
        aimTargetYaw = aimTargetPitch = 0f;
        PlayerCamera.Follow = CameraTarget;
        AimCamera.Follow = CameraTarget;
        player = transform.parent.GetComponent<Player>();
        playerTransposerCameras = PlayerCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        playerPOV = PlayerCamera.GetCinemachineComponent<CinemachinePOV>();
    }

    private void Start()
    {
        m_TargetZoom = CameraSO.maxZoom;
        transform.SetParent(null);
        CameraMain = Camera.main;

        player.playerInputAction.Look.performed += Look_performed;
        player.playerInputAction.Zoom.performed += Zoom_performed;
    }

    private void Update3rdPersonCam()
    {
        aimTargetYaw = Mathf.Clamp(aimTargetYaw, float.MinValue, float.MaxValue);
        aimTargetPitch = Mathf.Clamp(aimTargetPitch, playerPOV.m_VerticalAxis.m_MinValue, playerPOV.m_VerticalAxis.m_MaxValue);
        CameraTarget.transform.rotation = Quaternion.Euler(aimTargetPitch, aimTargetYaw, 0f);
    }
    private void Look_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Vector2 look = player.playerInputAction.Look.ReadValue<Vector2>();

        aimTargetPitch += look.y * Time.deltaTime * CameraSO.CameraAimData.RotationSpeed;
        aimTargetYaw += look.x * Time.deltaTime * CameraSO.CameraAimData.RotationSpeed;
    }

    private void OnDestroy()
    {
        player.playerInputAction.Look.performed -= Look_performed;
        player.playerInputAction.Zoom.performed -= Zoom_performed;
    }

    private void ReadScroll()
    {
        m_TargetZoom += player.playerInputAction.Zoom.ReadValue<Vector2>().y;
    }

    private void UpdateZoomCamera()
    {
        m_TargetZoom = Mathf.Clamp(m_TargetZoom, CameraSO.minZoom, CameraSO.maxZoom);
        playerTransposerCameras.m_CameraDistance = Mathf.Lerp(playerTransposerCameras.m_CameraDistance, m_TargetZoom, Time.deltaTime * CameraSO.zoomSoothing);
    }

    private void Zoom_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        ReadScroll();
    }

    // Update is called once per frame
    void Update()
    {
        Update3rdPersonCam();
        UpdateZoomCamera();
    }
}
