using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private int amountToPool;
    private List<GameObject> pooledObjects;

    public delegate void CreateEvent(GameObject go);
    public CreateEvent ObjectCreated;

    public ObjectPool(GameObject objectPool, Transform ParentTransform, int amountToPool = 0)
    {
        pooledObjects = new List<GameObject>();
        this.amountToPool = amountToPool;
        ObjectPoolManager opm = ObjectPoolManager.instance;

        for (int i = 0; i < this.amountToPool; i++)
        {
            GameObject tmp = opm.CreateGameObject(objectPool, ParentTransform);
            tmp.SetActive(false);
            opm.CallInvokeCreation(this, tmp);
            pooledObjects.Add(tmp);
        }
    }

    public void CallEvent(GameObject tmp)
    {
        ObjectCreated?.Invoke(tmp);
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].gameObject.activeInHierarchy)
            {
                return pooledObjects[i].gameObject;
            }
        }
        return null;
    }
}
