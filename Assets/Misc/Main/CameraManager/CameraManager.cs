using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [field: SerializeField] public Transform CameraTarget { get; private set; }
    [field: SerializeField] public CameraSO CameraSO { get; private set; }
    [field: SerializeField] public Player Player { get; private set; }

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
        playerTransposerCameras = PlayerCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        playerPOV = PlayerCamera.GetCinemachineComponent<CinemachinePOV>();
    }

    private IEnumerator ToggleAimCameraDelayCoroutine(bool enable, float time)
    {
        yield return new WaitForSeconds(time);
        EnableAimCamera(enable);
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

    private void Start()
    {
        m_TargetZoom = CameraSO.maxZoom;
        transform.SetParent(null);
        CameraMain = Camera.main;

        Player.PlayerController.playerInputAction.Zoom.performed += Zoom_performed;
    }

    private void OnDestroy()
    {
        Player.PlayerController.playerInputAction.Zoom.performed -= Zoom_performed;
    }

    private void UpdateZoomCamera()
    {
        m_TargetZoom = Mathf.Clamp(m_TargetZoom, CameraSO.minZoom, CameraSO.maxZoom);
        playerTransposerCameras.m_CameraDistance = Mathf.SmoothStep(playerTransposerCameras.m_CameraDistance, m_TargetZoom, Time.deltaTime * CameraSO.zoomSoothing);
    }

    private void Zoom_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        m_TargetZoom += Player.PlayerController.playerInputAction.Zoom.ReadValue<Vector2>().y;
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateZoomCamera();
    }

}
