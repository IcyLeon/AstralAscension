using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interact<T> : MonoBehaviour
{
    [Header("Interactions Data")]
    private Dictionary<Collider, T> Interact_List;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out T IInteractable))
        {
            Interact_List.Add(other, IInteractable);
            OnInteractEnter?.Invoke(other);
        }
    }

    protected bool isInteractableObject()
    {
        if (closestInteractionTransform == null)
            return false;

        return closestInteractionTransform.GetComponent<T>() != null;
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

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out T IInteractable))
        {
            Interact_List.Remove(other);
            OnInteractExit?.Invoke(other);
        }
    }
}
