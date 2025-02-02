using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

[RequireComponent(typeof(CinemachineVirtualCamera))]
[DisallowMultipleComponent]
public abstract class CameraPanVirtualCam : MonoBehaviour
{
    protected float cameraRotationSpeed;
    protected CameraPanManager cameraPanManager;
    protected float cameraRotationSmoothingSpeed;
    private Quaternion originalRotation;
    private Quaternion currentRotation;

    public CinemachineVirtualCamera VirtualCamera { get; private set; }
    public Cinemachine3rdPersonFollow Cinemachine3rdPersonFollow { get; private set; }
    private CinemachineCameraOffset cinemachineCameraOffset;

    protected virtual void Awake()
    {
        Init();
        VirtualCamera = GetComponent<CinemachineVirtualCamera>();
        Cinemachine3rdPersonFollow = VirtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        cinemachineCameraOffset = VirtualCamera.AddComponent<CinemachineCameraOffset>();
        originalRotation = Cinemachine3rdPersonFollow.FollowTargetRotation;
        ResetRotation();
    }

    private void Init()
    {
        cameraPanManager = GetComponentInParent<CameraPanManager>();

        if (cameraPanManager == null)
        {
            Debug.LogError(gameObject.name + "is not the child of CameraPanManager!");
            return;
        }

        cameraRotationSpeed = cameraPanManager.CameraPanSelectionDataSO.CameraRotationSpeed;
        cameraRotationSmoothingSpeed = cameraPanManager.CameraPanSelectionDataSO.CameraRotationSmoothingSpeed;
    }

    public virtual void OnEnter()
    {
        ResetRotation();
        gameObject.SetActive(true);
    }

    public virtual void OnExit()
    {
        gameObject.SetActive(false);
    }

    public virtual void OnDrag(Vector2 delta)
    {
        Vector3 rotationAngles = currentRotation.eulerAngles;

        rotationAngles.x += Time.unscaledDeltaTime * cameraRotationSpeed * delta.y * -1f; 
        rotationAngles.y += Time.unscaledDeltaTime * cameraRotationSpeed * delta.x;       

        if (rotationAngles.x > 180f)
            rotationAngles.x -= 360f;
        if (rotationAngles.x < -180f)
            rotationAngles.x += 360f;

        rotationAngles.x = Mathf.Clamp(rotationAngles.x, -65f, 65f);

        currentRotation = Quaternion.Euler(rotationAngles.x, rotationAngles.y, rotationAngles.z);
    }


    private void ResetRotation()
    {
        currentRotation = originalRotation;
    }

    public virtual void OnScroll(float delta)
    {

    }

    protected virtual void Update()
    {
        VirtualCamera.Follow.rotation = Quaternion.Lerp(VirtualCamera.Follow.rotation, currentRotation, Time.unscaledDeltaTime * cameraRotationSmoothingSpeed);
    }
}
