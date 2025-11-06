using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class MainCamera : MonoBehaviour
{
    public Camera Camera { get; private set; }
    public event Action<MainCamera> OnCameraDestroy;

    private void Awake()
    {
        Camera = GetComponent<Camera>();
        Camera.scene = gameObject.scene;
    }


    private void OnEnable()
    {
        RegisterCamera();
    }

    private void OnDisable()
    {
        HiddenCamera();
    }

    private void RegisterCamera()
    {
        CameraManager cameraManager = CameraManager.instance;

        if (cameraManager == null)
            return;

        cameraManager.Register(this);
    }

    private void HiddenCamera()
    {
        OnCameraDestroy?.Invoke(this);
        OnCameraDestroy = null;
    }



    private void OnDestroy()
    {
        HiddenCamera();
    }
}
