using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageManager : MonoBehaviour
{
    [SerializeField] private GameObject DamageTextPrefab;
    [SerializeField] private GameObject DamageTextHandlerParentPrefab;
    private ObjectPool<DamageText> ObjectPool;

    public class DamageInfo : EventArgs
    {
        public float DamageValue;
        public Vector3 WorldPosition;
    }
    public static EventHandler<DamageInfo> DamageChanged;

    private void Awake()
    {
        DamageChanged += DamageManager_DamageChanged;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Init();
    }

    private void Init()
    {
        Canvas canvas = GetCanvasGO();
        int AmountToPool = 15;

        if (canvas == null)
        {
            ObjectPool = new ObjectPool<DamageText>(DamageTextPrefab, null, AmountToPool);
            return;
        }

        GameObject Parent = Instantiate(DamageTextHandlerParentPrefab, canvas.transform);
        ObjectPool = new ObjectPool<DamageText>(DamageTextPrefab, Parent.transform, AmountToPool);
    }
    private Canvas GetCanvasGO()
    {
        GameObject go = GameObject.Find("Canvas");
        if (go == null)
            return null;

        return go.GetComponent<Canvas>();
    }

    private void DamageManager_DamageChanged(object sender, DamageInfo e)
    {
        DamageText damageText = ObjectPool.GetPooledObject();

        if (damageText == null)
            return;

        damageText.SetDamageTextValue(e.DamageValue.ToString(), e.WorldPosition);
    }

    private void OnDestroy()
    {
        DamageChanged -= DamageManager_DamageChanged;
    }
}
