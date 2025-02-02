using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactSphereMouseMotion : MonoBehaviour
{
    private PlayerController playerController;
    private ArtifactsRingRotation artifactsRingRotation;
    private Vector2 deltaMouseDirection;
    private Vector2 previousMousePos;

    private void Awake()
    {
        artifactsRingRotation = GetComponent<ArtifactsRingRotation>();
    }

    private void OnEnable()
    {
        playerController = PlayerController.instance;
        OnSubscribeEvents();
    }

    private void ArtifactSpin_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        StopRotation();
        previousMousePos = obj.ReadValue<Vector2>();
    }

    private void ArtifactSpin_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        StartRotation();
        artifactsRingRotation.AddRotateTorqueForce(deltaMouseDirection * Time.unscaledDeltaTime);
    }

    private void OnDisable()
    {
        OnUnsubscribeEvents();
    }

    private void ArtifactSpin_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Vector2 mousePos = obj.ReadValue<Vector2>();
        deltaMouseDirection = mousePos - previousMousePos;
        Vector2 delta = deltaMouseDirection;
        delta.x *= -1;
        artifactsRingRotation.Rotate(delta.x * Time.unscaledDeltaTime * 2f);
        previousMousePos = mousePos;
    }

    private void StopRotation()
    {
        if (artifactsRingRotation == null)
            return;

        artifactsRingRotation.enabled = false;
    }

    private void StartRotation()
    {
        if (artifactsRingRotation == null)
            return;

        artifactsRingRotation.enabled = true;
    }

    private void OnSubscribeEvents()
    {
        playerController.characterDisplayInputAction.ArtifactSpin.performed += ArtifactSpin_performed;
        playerController.characterDisplayInputAction.ArtifactSpin.started += ArtifactSpin_started;
        playerController.characterDisplayInputAction.ArtifactSpin.canceled += ArtifactSpin_canceled;
    }

    private void OnUnsubscribeEvents()
    {
        playerController.characterDisplayInputAction.ArtifactSpin.performed -= ArtifactSpin_performed;
        playerController.characterDisplayInputAction.ArtifactSpin.started -= ArtifactSpin_started;
        playerController.characterDisplayInputAction.ArtifactSpin.canceled -= ArtifactSpin_canceled;
    }
}
