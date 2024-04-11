using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interact : MonoBehaviour
{
    [Header("Interactions Data")]
    private Dictionary<Transform, Collider> Interact_List;
    [SerializeField] private LayerMask InteractLayers;
    [SerializeField] private float InteractionRange = 1f;
    [SerializeField] private SphereCollider InteractionsCollider;

    public delegate void OnInteractEvent(Collider Collider);
    public OnInteractEvent OnInteractEnter;
    public OnInteractEvent OnInteractExit;

    public Transform closestInteractionTransform { get; private set; }

    private void Awake()
    {
        Interact_List = new();
        InteractionsCollider.radius = InteractionRange;
    }

    protected virtual void Start()
    {
    }

    public Collider GetObject(Transform transform)
    {
        if (transform != null && Interact_List.TryGetValue(transform, out Collider value))
            return value;

        return null;
    }

    private Transform GetClosestTarget(Vector3 position)
    {
        float nearestDistance = Mathf.Infinity;
        Transform targetTransform = null;

        foreach (var Collider in Interact_List.Keys)
        {
            float distance = (Collider.transform.position - position).sqrMagnitude;
            if (distance < nearestDistance)
            {
                targetTransform = Collider.transform;
                nearestDistance = distance;
            }
        }

        return targetTransform;
    }

    private void Update()
    {
        closestInteractionTransform = GetClosestTarget(InteractionsCollider.bounds.center);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & InteractLayers) != 0)
        {
            Interact_List.Add(other.transform, other);
            OnInteractEnter?.Invoke(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Interact_List.Remove(other.transform))
        {
            OnInteractExit?.Invoke(other);
        }
    }
}
