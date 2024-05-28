using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DisallowMultipleComponent]
public abstract class InteractSensor : MonoBehaviour
{
    [Header("Interactions Data")]
    private Dictionary<Transform, Collider> Interact_List;
    [SerializeField] private LayerMask InteractLayers;
    [SerializeField] private float InteractionRange = 1f;
    [SerializeField] private SphereCollider InteractionsCollider;

    public delegate void OnInteractEvent(Collider Collider);
    public event OnInteractEvent OnInteractEnter;
    public event OnInteractEvent OnInteractExit;

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

        for (int i = 0; i < Interact_List.Keys.Count; i++)
        {
            Transform currentTransform = Interact_List.ElementAt(i).Key;
            if (currentTransform == null)
            {
                Interact_List.Remove(currentTransform);
                continue;
            }
            float distance = (currentTransform.transform.position - position).sqrMagnitude;
            if (distance < nearestDistance)
            {
                targetTransform = currentTransform.transform;
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
            if (!Interact_List.ContainsKey(other.transform))
            {
                Interact_List.Add(other.transform, other);
                OnInteractEnter?.Invoke(other);
            }
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
