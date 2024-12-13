using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

[RequireComponent(typeof(SmoothRigTransition))]
public abstract class POIRig : MonoBehaviour
{
    protected InteractSensor interactSensorReference;

    [Header("Rig Data")]
    [Range(0f, 180f)]
    [SerializeField] private float FOVAngleXZ = 90f;
    [Range(0f, 90f)]
    [SerializeField] private float FOVAngleY = 65f;
    private SmoothRigTransition SmoothRigTransition;

    protected IPointOfInterest closestPointOfInterest;

    protected virtual void Awake()
    {
        SmoothRigTransition = GetComponent<SmoothRigTransition>();
    }

    private void Start()
    {
        interactSensorReference.OnPOIChanged += OnPOIChanged;
        OnPOIChanged();
    }

    protected virtual void OnDestroy()
    {
        interactSensorReference.OnPOIChanged -= OnPOIChanged;
    }

    private void OnPOIChanged()
    {
        if (interactSensorReference == null)
            return;

        closestPointOfInterest = interactSensorReference.currentClosestPOI;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        UpdateTargetWeight();
        MoveTarget();
    }

    protected virtual bool CanMoveHead()
    {
        return true;
    }

    private Vector3 GetOriginalTargetPosition()
    {
        return SmoothRigTransition.GetConstraintObject().position + transform.forward * 0.5f;
    }

    private Vector3 GetClosestTransformPosition()
    {
        if (closestPointOfInterest == null)
            return GetOriginalTargetPosition();

        Vector3 LookAtPosition = closestPointOfInterest.GetIPointOfInterestTransform().position;

        Vector3 dir = LookAtPosition - SmoothRigTransition.GetConstraintObject().position;
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
        SmoothRigTransition.SetTargetPosition(GetClosestTransformPosition());
    }

    private void UpdateTargetWeight()
    {
        if (!CanMoveHead() || closestPointOfInterest == null)
        {
            SmoothRigTransition.SetTargetWeight(0f);
            return;
        }

        SmoothRigTransition.SetTargetWeight(1f);
    }
}
