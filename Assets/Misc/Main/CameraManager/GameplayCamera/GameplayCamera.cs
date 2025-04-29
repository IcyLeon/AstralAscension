using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class GameplayCamera : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera { get; private set; }
    protected PlayerCameraManager playerCameraManager;

    private void Awake()
    {
        Init();
    }

    protected virtual void Start()
    {

    }

    private void Init()
    {
        if (playerCameraManager != null)
            return;

        playerCameraManager = GetComponentInParent<PlayerCameraManager>();
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
