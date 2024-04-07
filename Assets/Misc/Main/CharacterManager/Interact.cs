using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [Header("Interactions Data")]
    [SerializeField] private float InteractionRange = 1f;
    [SerializeField] private SphereCollider InteractionsCollider;
    
    public delegate void OnInteractEvent(Collider Collider);
    public OnInteractEvent OnInteractEnter;
    public OnInteractEvent OnInteractExit;

    private void Awake()
    {
        InteractionsCollider.radius = InteractionRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        OnInteractEnter?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnInteractExit?.Invoke(other);
    }
}
