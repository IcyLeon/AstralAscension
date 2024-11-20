using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public abstract class CameraPanVirtualCam : MonoBehaviour
{
    protected float cameraRotationSpeed;
    protected CameraPanManager cameraPanManager;
    protected float cameraRotationSmoothingSpeed;
    private Quaternion originalRotation;
    private Quaternion currentRotation;

    public CinemachineVirtualCamera VirtualCamera { get; private set; }
    public Cinemachine3rdPersonFollow Cinemachine3rdPersonFollow { get; private set; }

    protected virtual void Awake()
    {
        cameraPanManager = GetComponentInParent<CameraPanManager>();

        if (cameraPanManager == null)
        {
            Debug.LogError(gameObject.name + "is not the child of CameraPanManager!");
            return;
        }

        VirtualCamera = GetComponent<CinemachineVirtualCamera>();
        Cinemachine3rdPersonFollow = VirtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        originalRotation = Cinemachine3rdPersonFollow.FollowTargetRotation;
        cameraRotationSpeed = cameraPanManager.CameraPanSelectionDataSO.CameraRotationSpeed;
        cameraRotationSmoothingSpeed = cameraPanManager.CameraPanSelectionDataSO.CameraRotationSmoothingSpeed;
        ResetRotation();
    }

    protected virtual void OnEnable()
    {
        ResetRotation();
    }

    protected virtual void OnDisable()
    {
    }

    public virtual void OnDrag(Vector2 delta)
    {
        Vector3 rotationAngles = currentRotation.eulerAngles;

        rotationAngles.x += Time.deltaTime * cameraRotationSpeed * delta.y * -1f; 
        rotationAngles.y += Time.deltaTime * cameraRotationSpeed * delta.x;       

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
        VirtualCamera.Follow.rotation = Quaternion.Lerp(VirtualCamera.Follow.rotation, currentRotation, Time.deltaTime * cameraRotationSmoothingSpeed);
    }
}
