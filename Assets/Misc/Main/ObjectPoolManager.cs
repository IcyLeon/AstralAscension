using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    public static void SetGlobalScale(Transform transform, Vector3 globalScale)
    {
        if (transform.parent == null)
        {
            transform.localScale = globalScale;
        }
        else
        {
            Vector3 parentGlobalScale = transform.parent.lossyScale;
            transform.localScale = new Vector3(
                globalScale.x / parentGlobalScale.x,
                globalScale.y / parentGlobalScale.y,
                globalScale.z / parentGlobalScale.z
            );
        }
    }

    public static GameObject CreateGameObject(GameObject prefab, Transform transform)
    {
        GameObject go = Instantiate(prefab, transform);
        return go;
    }

    public static GameObject CreateGameObject(string prefabName, Transform transform)
    {
        GameObject go = Instantiate(Resources.Load<GameObject>(prefabName), transform);
        return go;
    }

    internal IEnumerator InvokeCreationCoroutine<T>(ObjectPool<T> ObjectPool, T createdObj) where T : MonoBehaviour
    {
        yield return null;
        ObjectPool.CallObjectCreated(createdObj);
        createdObj.gameObject.SetActive(false);
    }

    internal void CallInvokeCreationDelay<T>(ObjectPool<T> objectPool, T createdObj) where T : MonoBehaviour
    {
        StartCoroutine(InvokeCreationCoroutine(objectPool, createdObj));
    }
}
