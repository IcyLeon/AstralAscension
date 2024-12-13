using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DisallowMultipleComponent]
public abstract class InteractSensor : MonoBehaviour
{
    [Header("Interactions Data")]
    private Dictionary<Transform, IPointOfInterest> POI_List;

    [SerializeField] private float InteractionRange = 1f;
    [SerializeField] private SphereCollider InteractionsCollider;

    public delegate void OnInteractEvent(Collider Collider);
    public event OnInteractEvent OnPOIInteractEnter;
    public event OnInteractEvent OnPOIInteractExit;

    public event Action OnPOIChanged;

    private IPointOfInterest prevClosestPOI;
    public IPointOfInterest currentClosestPOI { get; private set; }

    protected virtual void Awake()
    {
        POI_List = new();
        InteractionsCollider.radius = InteractionRange;
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

        for (int i = 0; i < POI_List.Keys.Count; i++)
        {
            Transform currentTransform = POI_List.ElementAt(i).Key;

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
        currentClosestPOI = GetClosestTarget(InteractionsCollider.bounds.center);

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
