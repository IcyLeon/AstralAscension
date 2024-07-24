using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

[RequireComponent(typeof(SmoothRigTransition))]
public abstract class POIRig : MonoBehaviour
{
    protected InteractSensor interactSensorReference;
    private const float offsetDistance = 0.35f;

    [Header("Rig Data")]
    [Range(0f, 180f)]
    [SerializeField] private float FOVAngleXZ = 90f;
    [Range(0f, 90f)]
    [SerializeField] private float FOVAngleY = 65f;
    [SerializeField] private MultiAimConstraint MultiAimConstraint;
    private SmoothRigTransition SmoothRigTransition;
    [SerializeField] private float MoveTowardsSoothingTime = 1f;

    private IPointOfInterest closestPointOfInterest;
    private Transform currentTransform;

    private Vector3 closestTarget;

    protected virtual void Awake()
    {
        SmoothRigTransition = GetComponent<SmoothRigTransition>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        UpdateTarget();
        MoveTarget();
    }

    protected virtual bool CanMoveHead()
    {
        return true;
    }

    private Vector3 GetOriginalTargetPosition()
    {
        return MultiAimConstraint.data.constrainedObject.position + transform.forward * offsetDistance;
    }

    private Vector3 GetClosestTransformPosition()
    {
        if (interactSensorReference == null || interactSensorReference.closestInteractionTransform == null)
            return GetOriginalTargetPosition();

        Vector3 LookAtPosition = interactSensorReference.closestInteractionTransform.position;

        if (currentTransform != interactSensorReference.closestInteractionTransform)
        {
            currentTransform = interactSensorReference.closestInteractionTransform;
            closestPointOfInterest = currentTransform.GetComponent<IPointOfInterest>();
        }

        if (closestPointOfInterest != null)
        {
            LookAtPosition = closestPointOfInterest.GetIPointOfInterestTransform().position;
        }

        Vector3 dir = LookAtPosition - MultiAimConstraint.data.constrainedObject.position;
        if (IsInFOV(dir.normalized))
        {
            return LookAtPosition + offsetDistance * dir.normalized;
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
        Vector3 WorldPosition = Vector3.MoveTowards(MultiAimConstraint.data.sourceObjects[0].transform.position, closestTarget, Time.deltaTime * MoveTowardsSoothingTime);
        SetTargetPosition(WorldPosition);
    }

    private void UpdateTarget()
    {
        closestTarget = GetClosestTransformPosition();

        if (!CanMoveHead() || closestTarget == GetOriginalTargetPosition())
        {
            SmoothRigTransition.SetTargetWeight(0f);
            return;
        }

        SmoothRigTransition.SetTargetWeight(1f);
    }

    public void SetTargetPosition(Vector3 WorldPosition)
    {
        MultiAimConstraint.data.sourceObjects[0].transform.position = WorldPosition;
    }
}
