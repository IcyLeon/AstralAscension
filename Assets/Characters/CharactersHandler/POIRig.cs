using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIRig : MonoBehaviour
{
    protected Characters Characters;
    [Range(0f, 180f)]
    [SerializeField] private float FOVAngle = 90f;
    [SerializeField] private AimRig AimRig;
    [SerializeField] private Transform Target;
    [SerializeField] private float MoveTowardsSoothingTime = 1f;

    // Start is called before the first frame update
    private void Awake()
    {
        Characters = GetCharacters();
    }

    private Characters GetCharacters()
    {
        Transform currentTransform = transform;

        while (currentTransform != null)
        {
            if (currentTransform.TryGetComponent(out Characters characters))
            {
                return characters;
            }
            currentTransform = currentTransform.transform.parent;
        }
        return null;
    }


    // Update is called once per frame
    private void LateUpdate()
    {
        UpdateTarget();
    }

    protected virtual bool CanMoveHead()
    {
        return true;
    }

    public Transform GetClosestTransform()
    {
        if (Characters == null)
            return null;

        if (Characters.closestInteractionTransform == null)
            return null;

        Vector3 dir = Characters.closestInteractionTransform.position - Characters.transform.position;
        if (Vector3.Angle(Characters.transform.forward, dir) <= FOVAngle / 2f)
        {
            return Characters.closestInteractionTransform;
        }

        return null;
    }

    private void UpdateTarget()
    {
        if (!CanMoveHead())
        {
            AimRig.SetTargetWeight(0f);
            return;
        }

        Transform closestTarget = GetClosestTransform();
        if (closestTarget == null)
        {
            AimRig.SetTargetWeight(0f);
            return;
        }
        Target.transform.position = Vector3.MoveTowards(Target.transform.position, closestTarget.transform.position, Time.deltaTime * MoveTowardsSoothingTime);
        AimRig.SetTargetWeight(1f);
    }

    public void SetTargetPosition(Vector3 WorldPosition)
    {
        Target.transform.position = WorldPosition;
    }
}
