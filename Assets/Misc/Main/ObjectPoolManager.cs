using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

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

    internal IEnumerator InvokeCreationCoroutine<T>(ObjectPool<T> ObjectPool, T createdObj) where T : MonoBehaviour
    {
        yield return null;
        ObjectPool.ObjectCreated?.Invoke(createdObj);
        createdObj.gameObject.SetActive(false);
    }

    internal void CallInvokeCreation<T>(ObjectPool<T> objectPool, T createdObj) where T : MonoBehaviour
    {
        StartCoroutine(InvokeCreationCoroutine(objectPool, createdObj));
    }
}
