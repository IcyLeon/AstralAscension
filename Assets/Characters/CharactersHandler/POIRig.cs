using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public abstract class POIRig : MonoBehaviour
{
    protected Interact interactReference;

    [Header("Rig Data")]
    [Range(0f, 180f)]
    [SerializeField] private float FOVAngle = 90f;
    [SerializeField] private MultiAimConstraint MultiAimConstraint;
    [SerializeField] private AimRig AimRig;
    [SerializeField] private float MoveTowardsSoothingTime = 1f;

    private IPointOfInterest closestPointOfInterest;
    private Transform currentTransform;

    private Vector3 originalTargetPosition;
    private Vector3 closestTarget;

    private void Awake()
    {
        originalTargetPosition = MultiAimConstraint.data.constrainedObject.position + transform.forward * 0.35f;
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

    private Vector3 GetClosestTransformPosition()
    {
        if (interactReference == null)
            return originalTargetPosition;

        if (interactReference.closestInteractionTransform == null)
            return originalTargetPosition;

        Vector3 LookAtPosition = interactReference.closestInteractionTransform.position;

        if (currentTransform != interactReference.closestInteractionTransform)
        {
            currentTransform = interactReference.closestInteractionTransform;
            closestPointOfInterest = currentTransform.GetComponent<IPointOfInterest>();
        }

        if (closestPointOfInterest != null)
        {
            LookAtPosition = closestPointOfInterest.GetIPointOfInterestTransform().position;
        }

        Vector3 dir = LookAtPosition - MultiAimConstraint.data.constrainedObject.position;
        if (Vector3.Angle(interactReference.transform.forward, dir.normalized) <= FOVAngle / 2f)
        {
            return LookAtPosition;
        }

        return originalTargetPosition;
    }

    private void MoveTarget()
    {
        Vector3 WorldPosition = Vector3.MoveTowards(MultiAimConstraint.data.sourceObjects[0].transform.position, closestTarget, Time.deltaTime * MoveTowardsSoothingTime);
        SetTargetPosition(WorldPosition);
    }

    private void UpdateTarget()
    {
        closestTarget = GetClosestTransformPosition();

        if (!CanMoveHead())
        {
            AimRig.SetTargetWeight(0f);
            return;
        }

        if (closestTarget == originalTargetPosition)
        {
            AimRig.SetTargetWeight(0f);
            return;
        }

        AimRig.SetTargetWeight(1f);
    }

    public void SetTargetPosition(Vector3 WorldPosition)
    {
        MultiAimConstraint.data.sourceObjects[0].transform.position = WorldPosition;
    }
}
