using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraManager : MonoBehaviour
{
    [field: SerializeField] public Transform CameraTarget { get; private set; }
    [field: SerializeField] public CameraSO CameraSO { get; private set; }

    private GameplayCamera gameplayPlayerCamera;
    private GameplayCamera gameplayAimCamera;
    private GameplayCamera currentCamera;
    public Camera cameraMain { get; private set; }

    private PlayerController controller;
    public PlayerController playerController
    {
        get
        {
            if (controller == null)
            {
                Player player = GetComponentInParent<Player>();
                controller = player.playerController;
            }

            return controller;
        }
    }


    private Coroutine toggleAimCameraCoroutine;

    private void Awake()
    {
        cameraMain = Camera.main;
        gameplayPlayerCamera = GetComponentInChildren<GameplayPlayerCamera>(true);
        gameplayAimCamera = GetComponentInChildren<GameplayAimCamera>(true);
        DisableAllCamera();
    }

    private void DisableAllCamera()
    {
        GameplayCamera[] GameplayCameralist = GetComponentsInChildren<GameplayPlayerCamera>(true);

        foreach (var camera in GameplayCameralist)
        {
            camera.gameObject.SetActive(false);
        }
    }

    public Vector3 GetTargetCameraRayPosition(float maxDistance)
    {
        Ray ray = cameraMain.ScreenPointToRay(new Vector2(Screen.width / 2f, Screen.height / 2f));
        return ray.origin + ray.direction * maxDistance;
    }

    public void ChangeCamera(GameplayCamera cam)
    {
        if (currentCamera != null)
        {
            currentCamera.gameObject.SetActive(false);
        }

        currentCamera = cam;

        currentCamera.gameObject.SetActive(true);
    }

    private void Start()
    {
        transform.SetParent(null);
        ChangeCamera(gameplayPlayerCamera);
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
        if (enable)
        {
            ChangeCamera(gameplayAimCamera);
            return;
        }

        ChangeCamera(gameplayPlayerCamera);
    }
}
