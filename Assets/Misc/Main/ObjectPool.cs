using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private int amountToPool;
    private List<T> pooledObjects = new();

    public delegate void CreateEvent(GameObject go);
    public CreateEvent ObjectCreated;

    public ObjectPool(GameObject objectPool, Transform ParentTransform = null, int amountToPool = 1)
    {
        this.amountToPool = amountToPool;
        ObjectPoolManager opm = ObjectPoolManager.instance;

        for (int i = 0; i < this.amountToPool; i++)
        {
            GameObject tmp = opm.CreateGameObject(objectPool, ParentTransform);
            opm.CallInvokeCreation(this, tmp);
            pooledObjects.Add(tmp.GetComponent<T>());
        }
    }

    public T GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].gameObject.activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
