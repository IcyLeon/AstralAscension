using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    public GameObject CreateGameObject(GameObject prefab, Transform transform)
    {
        GameObject go = Instantiate(prefab, transform);
        return go;
    }

    internal IEnumerator InvokeCreationCoroutine<T>(ObjectPool<T> ObjectPool, GameObject createdObj) where T : MonoBehaviour
    {
        yield return null;
        CallCreationObjectEvent(ObjectPool, createdObj);
    }

    internal void CallCreationObjectEvent<T>(ObjectPool<T> ObjectPool, GameObject tmp) where T : MonoBehaviour
    {
        ObjectPool.ObjectCreated?.Invoke(tmp);
        tmp.SetActive(false);
    }

    internal void CallInvokeCreation<T>(ObjectPool<T> objectPool, GameObject createdObj) where T : MonoBehaviour
    {
        StartCoroutine(InvokeCreationCoroutine(objectPool, createdObj));
    }
}
