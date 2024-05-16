using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ObjectPoolManager;

public class ObjectPool<T> where T : MonoBehaviour
{
    private int amountToPool;
    private List<T> pooledObjects = new();

    public delegate void CreateEvent(T go);
    public event CreateEvent ObjectCreated;

    /// <summary>
    /// Call this in Start(), DO NOT PUT IT UNDER Awake()
    /// </summary>
    public ObjectPool(GameObject objectPool, Transform ParentTransform = null, int amountToPool = 1)
    {
        this.amountToPool = amountToPool;

        for (int i = 0; i < this.amountToPool; i++)
        {
            GameObject tmp = CreateGameObject(objectPool, ParentTransform);
            T createdObjComponent = tmp.GetComponent<T>();
            instance.CallInvokeCreationDelay(this, createdObjComponent);
            pooledObjects.Add(createdObjComponent);
        }
    }
    internal void CallObjectCreated(T objectType)
    {
        ObjectCreated?.Invoke(objectType);
    }

    public T GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].gameObject.activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    public T GetActivePooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (pooledObjects[i].gameObject.activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
