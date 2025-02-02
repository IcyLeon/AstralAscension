using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraManager : MonoBehaviour
{
    [field: SerializeField] public Transform CameraTarget { get; private set; }
    [field: SerializeField] public CameraSO CameraSO { get; private set; }
    public PlayerController playerController { get; private set; }

    private GameplayCamera gameplayPlayerCamera;
    private GameplayCamera gameplayAimCamera;
    private GameplayCamera[] GameplayCameralist;
    private GameplayCamera currentCamera;

    public Camera CameraMain { get; private set; }


    private Coroutine toggleAimCameraCoroutine;

    private void Awake()
    {
        CameraMain = Camera.main;
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
        playerController = PlayerController.instance;
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

    public bool IsAimCameraActive()
    {
        return currentCamera == gameplayAimCamera;
    }
}
