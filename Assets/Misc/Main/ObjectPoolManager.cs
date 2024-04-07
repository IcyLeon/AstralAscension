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

    private IEnumerator InvokeCreationCoroutine(ObjectPool ObjectPool, GameObject createdObj)
    {
        yield return null;
        ObjectPool.CallEvent(createdObj);
    }

    public void CallInvokeCreation(ObjectPool objectPool, GameObject createdObj)
    {
        StartCoroutine(InvokeCreationCoroutine(objectPool, createdObj));
    }
}
