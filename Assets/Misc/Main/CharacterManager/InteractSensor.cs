using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(SphereCollider))]
public class InteractSensor : MonoBehaviour
{
    [Header("Interactions Data")]
    private Dictionary<Transform, IPointOfInterest> POI_List;

    private SphereCollider interactionsCollider;

    public delegate void OnInteractEvent(Collider Collider);
    public event OnInteractEvent OnPOIInteractEnter;
    public event OnInteractEvent OnPOIInteractExit;

    public event Action OnPOIChanged;

    private IPointOfInterest prevClosestPOI;
    public IPointOfInterest currentClosestPOI { get; private set; }

    protected virtual void Awake()
    {
        POI_List = new();

        interactionsCollider = GetComponent<SphereCollider>();
        interactionsCollider.isTrigger = true;
    }

    public void CreateCollider(float radius, Vector3 localCenter)
    {
        interactionsCollider.radius = radius;
        interactionsCollider.center = localCenter;
    }

    protected virtual void Start()
    {
    }

    protected IPointOfInterest GetPOIObject(Transform transform)
    {
        if (transform != null && POI_List.TryGetValue(transform, out IPointOfInterest value))
            return value;

        return null;
    }

    private IPointOfInterest GetClosestTarget(Vector3 position)
    {
        float nearestDistance = Mathf.Infinity;
        IPointOfInterest targetPOI = null;

        foreach(var currentTransform in POI_List.Keys)
        {

            float distance = (currentTransform.transform.position - position).sqrMagnitude;

            if (distance < nearestDistance)
            {
                targetPOI = POI_List[currentTransform];
                nearestDistance = distance;
            }
        }

        return targetPOI;
    }

    private void Update()
    {
        UpdateClosestPOI();
    }

    private void UpdateClosestPOI()
    {
        currentClosestPOI = GetClosestTarget(interactionsCollider.bounds.center);

        if (prevClosestPOI != currentClosestPOI)
        {
            OnPOIChanged?.Invoke();
            prevClosestPOI = currentClosestPOI;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IPointOfInterest POI = other.GetComponent<IPointOfInterest>();

        if (POI == null)
            return;

        if (!POI_List.ContainsKey(other.transform))
        {
            POI_List.Add(other.transform, POI);
            OnPOIInteractEnter?.Invoke(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (POI_List.Remove(other.transform))
        {
            OnPOIInteractExit?.Invoke(other);
        }
    }
}
