using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class ObjectPool<T> where T : MonoBehaviour
{
    private int amountToPool;
    private List<T> pooledObjects;

    public ObjectPool(GameObject objectPool, Transform ParentTransform = null, int amountToPool = 1) : this(amountToPool)
    {
        for (int i = 0; i < this.amountToPool; i++)
        {
            GameObject go = Object.Instantiate(objectPool, ParentTransform);
            go.SetActive(false);
            pooledObjects.Add(go.GetComponent<T>());
        }
    }

    private ObjectPool(int amountToPool)
    {
        pooledObjects = new();
        this.amountToPool = amountToPool;
    }

    public ObjectPool(string objectPoolPrefabName, Transform ParentTransform = null, int amountToPool = 1) : this(amountToPool)
    {
        GameObject objectPool = Resources.Load<GameObject>(objectPoolPrefabName);

        for (int i = 0; i < this.amountToPool; i++)
        {
            GameObject go = Object.Instantiate(objectPool, ParentTransform);
            go.SetActive(false);
            pooledObjects.Add(go.GetComponent<T>());
        }
    }


    public T GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].gameObject.activeSelf)
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
