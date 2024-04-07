using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIRig : MonoBehaviour
{
    private Dictionary<Collider, IInteractable> POI_List;
    [SerializeField] private Characters characters;
    [SerializeField] private AimRig AimRig;
    [SerializeField] private Transform Target;
    [SerializeField] private float MoveTowardsSoothingTime = 1f;

    // Start is called before the first frame update
    private void Awake()
    {
        POI_List = new();
        Player.OnInteractionEnter += OnInteractionEnter;
        Player.OnInteractionExit += OnInteractionExit;
    }

    private void OnDestroy()
    {
        Player.OnInteractionEnter -= OnInteractionEnter;
        Player.OnInteractionExit -= OnInteractionExit;
    }

    private void OnInteractionEnter(Collider collider)
    {
        if (collider.TryGetComponent(out IInteractable IInteractable)) {
            POI_List.Add(collider, IInteractable);
        }
    }

    private void OnInteractionExit(Collider collider)
    {
        POI_List.Remove(collider);
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateTarget();
    }

    private void UpdateTarget()
    {
        Transform closestTarget = GetClosestTarget(characters.transform.position);
        if (closestTarget == null)
        {
            AimRig.SetTargetWeight(0f);
            return;
        }
        Target.transform.position = Vector3.MoveTowards(Target.transform.position, closestTarget.transform.position, Time.deltaTime * MoveTowardsSoothingTime);
        AimRig.SetTargetWeight(1f);
    }

    private Transform GetClosestTarget(Vector3 position)
    {
        float nearestDistance = Mathf.Infinity;
        Transform targetTransform = null;

        foreach(var Collider in POI_List.Keys)
        {
            float distance = Vector3.Distance(Collider.transform.position, position);
            if (distance < nearestDistance)
            {
                targetTransform = Collider.transform;
                nearestDistance = distance;
            }
        }

        return targetTransform;
    }

    public void SetTargetPosition(Vector3 WorldPosition)
    {
        Target.transform.position = WorldPosition;
    }
}
