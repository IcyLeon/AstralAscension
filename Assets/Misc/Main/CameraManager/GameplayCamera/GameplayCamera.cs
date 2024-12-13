using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayCamera : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera { get; private set; }
    protected CameraManager cameraManager;

    private void Awake()
    {
        Init();
    }

    protected virtual void Start()
    {

    }

    private void Init()
    {
        if (cameraManager != null)
            return;

        cameraManager = GetComponentInParent<CameraManager>();
        VirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    protected virtual void OnEnable()
    {
    }

    protected virtual void OnDisable()
    {
    }

    protected virtual void Update()
    {

    }


}
