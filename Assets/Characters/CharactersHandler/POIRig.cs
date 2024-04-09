using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIRig : MonoBehaviour
{
    [SerializeField] protected Characters Characters;
    [Range(0f, 180f)]
    [SerializeField] private float FOVAngle = 90f;
    [SerializeField] private AimRig AimRig;
    [SerializeField] private Transform Target;
    [SerializeField] private float MoveTowardsSoothingTime = 1f;

    private IPointOfInterest closestPointOfInterest;
    private Transform currentTransform;

    // Update is called once per frame
    private void LateUpdate()
    {
        UpdateTarget();
    }

    protected virtual bool CanMoveHead()
    {
        return true;
    }

    public Vector3 GetClosestTransformPosition()
    {
        if (Characters == null)
            return default(Vector3);

        if (Characters.closestInteractionTransform == null)
            return default(Vector3);

        Vector3 LookAtPosition = Characters.closestInteractionTransform.position;

        if (currentTransform != Characters.closestInteractionTransform)
        {
            closestPointOfInterest = Characters.closestInteractionTransform.GetComponent<IPointOfInterest>();
            currentTransform = Characters.closestInteractionTransform;
        }

        if (closestPointOfInterest != null)
        {
            LookAtPosition = closestPointOfInterest.GetIPointOfInterestTransform().position;
        }

        Vector3 dir = LookAtPosition - Characters.GetIPointOfInterestTransform().position;
        if (Vector3.Angle(Characters.transform.forward, dir) <= FOVAngle / 2f)
        {
            return LookAtPosition;
        }

        return default(Vector3);
    }

    private void UpdateTarget()
    {
        if (!CanMoveHead())
        {
            AimRig.SetTargetWeight(0f);
            return;
        }

        Vector3 closestTarget = GetClosestTransformPosition();
        if (closestTarget == default(Vector3))
        {
            AimRig.SetTargetWeight(0f);
            return;
        }
        Target.transform.position = Vector3.MoveTowards(Target.transform.position, closestTarget, Time.deltaTime * MoveTowardsSoothingTime);
        AimRig.SetTargetWeight(1f);
    }

    public void SetTargetPosition(Vector3 WorldPosition)
    {
        Target.transform.position = WorldPosition;
    }
}
