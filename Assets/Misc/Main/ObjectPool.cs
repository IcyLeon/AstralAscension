using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ObjectPoolManager;

public class ObjectPool<T> where T : MonoBehaviour
{
    private int amountToPool;
    private List<T> pooledObjects;

    public ObjectPool(GameObject objectPool, Transform ParentTransform = null, int amountToPool = 1)
    {
        pooledObjects = new();
        this.amountToPool = amountToPool;

        for (int i = 0; i < this.amountToPool; i++)
        {
            GameObject tmp = CreateGameObject(objectPool, ParentTransform);
            pooledObjects.Add(tmp.GetComponent<T>());
        }
    }
    public ObjectPool(string objectPoolPrefabName, Transform ParentTransform = null, int amountToPool = 1)
    {
        pooledObjects = new();
        this.amountToPool = amountToPool;

        for (int i = 0; i < this.amountToPool; i++)
        {
            GameObject tmp = CreateGameObject(Resources.Load<GameObject>(objectPoolPrefabName), ParentTransform);
            pooledObjects.Add(tmp.GetComponent<T>());
        }
    }


    public T GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].gameObject.activeInHierarchy)
            {
                pooledObjects[i].gameObject.SetActive(true);
                return pooledObjects[i];
            }
        }
        return null;
    }

    public void CallbackPoolObject(Action<T, int> action)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            action(pooledObjects[i], i);
        }
    }

    public void ResetAll()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            pooledObjects[i].gameObject.SetActive(false);
        }
    }

    public void Reset(T objectType)
    {
        objectType.gameObject.SetActive(false);
    }
}
