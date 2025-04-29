using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class POIRig : RigController
{
    private InteractSensor interactSensorReference;

    [Range(0f, 180f)]
    [SerializeField] private float FOVAngleXZ = 90f;
    [Range(0f, 90f)]
    [SerializeField] private float FOVAngleY = 65f;
    [SerializeField] private float soothingTargetSpeed = 1f;

    [SerializeField] private Transform TargetTransform;
    private Vector3 targetPosition;

    protected IPointOfInterest closestPointOfInterest;

    protected virtual void Awake()
    {
        interactSensorReference = GetComponentInParent<InteractSensor>();
    }

    protected virtual void OnEnable()
    {
        SubscribeEvent();
    }

    protected virtual void OnDisable()
    {
        UnsubscribeEvent();
    }

    private void SubscribeEvent()
    {
        if (interactSensorReference == null)
            return;

        interactSensorReference.OnPOIChanged += OnPOIChanged;
    }
    private void UnsubscribeEvent()
    {
        if (interactSensorReference == null)
            return;

        interactSensorReference.OnPOIChanged -= OnPOIChanged;
    }

    protected virtual void OnDestroy()
    {
        UnsubscribeEvent();
    }

    private void OnPOIChanged()
    {
        if (interactSensorReference == null)
            return;

        closestPointOfInterest = interactSensorReference.currentClosestPOI;
    }

    private void Update()
    {
        UpdateTargetPosition();
        UpdateTargetWeight();
    }

    private void LateUpdate()
    {
        MoveTarget();
    }

    protected virtual bool CanMoveHead()
    {
        return true;
    }

    private Vector3 GetOriginalTargetPosition()
    {
        return TargetTransform.position + transform.forward * 0.5f;
    }

    private Vector3 GetClosestTransformPosition()
    {
        if (closestPointOfInterest == null)
            return GetOriginalTargetPosition();

        Vector3 LookAtPosition = closestPointOfInterest.GetIPointOfInterestTransform().position;

        Vector3 dir = LookAtPosition - TargetTransform.position;
        if (IsInFOV(dir.normalized))
        {
            return LookAtPosition;
        }

        return GetOriginalTargetPosition();
    }

    private bool IsInFOV(Vector3 dir)
    {
        Vector3 direction = dir;
        Vector3 forwardXZ = interactSensorReference.transform.forward;
        forwardXZ.y = 0;
        direction.y = 0;

        float angle = Vector3Handler.FindAngleBetweenVectors(forwardXZ, direction);

        return angle <= FOVAngleXZ / 2f &&
            Mathf.Abs(Mathf.Asin(dir.y) * Mathf.Rad2Deg) <= FOVAngleY / 2;
    }

    private void MoveTarget()
    {
        SetTargetPosition(GetClosestTransformPosition());
    }

    private void UpdateTargetPosition()
    {
        TargetTransform.position = Vector3.Lerp(TargetTransform.position, targetPosition, Time.deltaTime * soothingTargetSpeed);
    }

    private void SetTargetPosition(Vector3 Position)
    {
        targetPosition = Position;
    }

    private void UpdateTargetWeight()
    {
        if (!CanMoveHead() || closestPointOfInterest == null)
        {
            SetTargetWeight(0f);
            return;
        }

        SetTargetWeight(1f);
    }
}
