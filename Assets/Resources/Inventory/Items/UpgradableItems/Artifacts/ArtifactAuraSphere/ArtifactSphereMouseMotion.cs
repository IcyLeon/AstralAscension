using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArtifactSphereMouseMotion : MonoBehaviour
{
    private UIController uiController;
    private ArtifactsRingRotation artifactsRingRotation;
    private Vector2 deltaMouseDirection;
    private Vector2 previousMousePos;
    private bool validPosition;

    private void Awake()
    {
        artifactsRingRotation = GetComponent<ArtifactsRingRotation>();
    }

    private void OnEnable()
    {
        uiController = UIController.instance;
        OnSubscribeEvents();
    }

    private void ArtifactSpin_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        validPosition = IsValid();

        if (!validPosition)
            return;

        previousMousePos = obj.ReadValue<Vector2>();
        DisableRotation();
    }

    private void ArtifactSpin_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!validPosition)
            return;

        EnableRotation();
        artifactsRingRotation.AddRotateTorqueForce(deltaMouseDirection * Time.unscaledDeltaTime);
    }

    private void OnDisable()
    {
        OnUnsubscribeEvents();
    }

    private void ArtifactSpin_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!validPosition)
            return;

        Vector2 mousePos = obj.ReadValue<Vector2>();
        Vector2 delta = mousePos - previousMousePos;

        if (delta.magnitude <= 0)
            return;

        deltaMouseDirection = delta;

        artifactsRingRotation.Rotate(-deltaMouseDirection.x * Time.unscaledDeltaTime * 1.5f);
        previousMousePos = mousePos;
    }

    private void DisableRotation()
    {
        if (artifactsRingRotation == null)
            return;

        artifactsRingRotation.DisableRotation();
    }

    private void EnableRotation()
    {
        if (artifactsRingRotation == null)
            return;

        artifactsRingRotation.EnableRotation();
    }

    private bool IsValid()
    {
        return !EventSystem.current.IsPointerOverGameObject();
    }

    private void OnSubscribeEvents()
    {
        uiController.characterDisplayInputAction.ArtifactSpin.performed += ArtifactSpin_performed;
        uiController.characterDisplayInputAction.ArtifactSpin.started += ArtifactSpin_started;
        uiController.characterDisplayInputAction.ArtifactSpin.canceled += ArtifactSpin_canceled;
    }

    private void OnUnsubscribeEvents()
    {
        uiController.characterDisplayInputAction.ArtifactSpin.performed -= ArtifactSpin_performed;
        uiController.characterDisplayInputAction.ArtifactSpin.started -= ArtifactSpin_started;
        uiController.characterDisplayInputAction.ArtifactSpin.canceled -= ArtifactSpin_canceled;
    }
}
