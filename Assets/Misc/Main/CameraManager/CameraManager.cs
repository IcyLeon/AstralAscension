using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [field: SerializeField] public Transform CameraTarget { get; private set; }
    [field: SerializeField] public CameraSO CameraSO { get; private set; }
    public PlayerController playerController { get; private set; }

    [Header("Cinemachine Camera Components")]
    [SerializeField] CinemachineVirtualCamera PlayerCamera;
    [SerializeField] CinemachineVirtualCamera AimCamera;
    private CinemachineFramingTransposer playerTransposerCameras;
    public CinemachinePOV playerPOV { get; private set; }

    private float m_TargetZoom;
    public Camera CameraMain { get; private set; }


    private Coroutine toggleAimCameraCoroutine;

    private void Awake()
    {
        CameraMain = Camera.main;
        m_TargetZoom = CameraSO.maxZoom;
        playerTransposerCameras = PlayerCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        playerPOV = PlayerCamera.GetCinemachineComponent<CinemachinePOV>();
    }

    private void Start()
    {
        playerController = PlayerController.instance;
        transform.SetParent(null);
        playerController.playerInputAction.Zoom.performed += Zoom_performed;
    }

    private IEnumerator ToggleAimCameraDelayCoroutine(bool enable, float time)
    {
        yield return new WaitForSeconds(time);
        EnableAimCamera(enable);
        toggleAimCameraCoroutine = null;
    }

    public void ToggleAimCamera(bool enable, float time = 0f)
    {
        if (toggleAimCameraCoroutine != null)
        {
            StopCoroutine(toggleAimCameraCoroutine);
        }

        if (time == 0f)
        {
            EnableAimCamera(enable);
            return;
        }

        toggleAimCameraCoroutine = StartCoroutine(ToggleAimCameraDelayCoroutine(enable, time));
    }


    private void EnableAimCamera(bool enable)
    {
        AimCamera.gameObject.SetActive(enable);
    }

    public bool IsAimCameraActive()
    {
        return AimCamera.gameObject.activeSelf;
    }


    private void OnDestroy()
    {
        playerController.playerInputAction.Zoom.performed -= Zoom_performed;
    }

    private void UpdateZoomCamera()
    {
        playerTransposerCameras.m_CameraDistance = Mathf.SmoothStep(playerTransposerCameras.m_CameraDistance, m_TargetZoom, Time.deltaTime * CameraSO.zoomSoothing);
    }

    private void Zoom_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        m_TargetZoom += playerController.playerInputAction.Zoom.ReadValue<Vector2>().y;
        m_TargetZoom = Mathf.Clamp(m_TargetZoom, CameraSO.minZoom, CameraSO.maxZoom);
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateZoomCamera();
    }

}
