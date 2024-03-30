using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CameraSO cameraSO;
    [SerializeField] CinemachineVirtualCamera PlayerCamera;
    private CinemachineFramingTransposer playerTransposerCameras;


    private float m_TargetZoom;
    public Camera CameraMain { get; private set; }
    [field: SerializeField] public Player Player { get; private set; }

    private void Awake()
    {
        playerTransposerCameras = PlayerCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    private void Start()
    {
        m_TargetZoom = cameraSO.maxZoom;
        transform.SetParent(null);
        CameraMain = Camera.main;

        Player.playerInputAction.Zoom.performed += Zoom_performed;
    }

    private void OnDestroy()
    {
        Player.playerInputAction.Zoom.performed -= Zoom_performed;
    }

    private void ReadScroll()
    {
        m_TargetZoom += Player.playerInputAction.Zoom.ReadValue<Vector2>().y;
        m_TargetZoom = Mathf.Clamp(m_TargetZoom, cameraSO.minZoom, cameraSO.maxZoom);
    }

    private void UpdateZoomCamera()
    {
        playerTransposerCameras.m_CameraDistance = Mathf.Lerp(playerTransposerCameras.m_CameraDistance, m_TargetZoom, Time.deltaTime * cameraSO.zoomSoothing);
    }

    private void Zoom_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        ReadScroll();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateZoomCamera();
    }
}
