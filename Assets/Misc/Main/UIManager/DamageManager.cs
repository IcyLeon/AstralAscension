using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static ElementalReactionsManager;

[DisallowMultipleComponent]
public class DamageManager : MonoBehaviour
{
    [SerializeField] private GameObject DamageTextPrefab;
    [SerializeField] private GameObject DamageTextHandlerParentPrefab;
    private ObjectPool<DamageText> ObjectPool;

    public class DamageInfo : EventArgs
    {
        public string DamageText;
        public ElementsInfoSO ElementsInfoSO;
        public Vector3 WorldPosition;
    }

    public static event EventHandler<DamageInfo> OnDamageTextSend;

    private void Awake()
    {
        OnDamageTextSend += DamageManager_OnDamageHit;
    }

    private void DamageManager_OnDamageHit(object sender, DamageInfo e)
    {
        DamageText damageText = ObjectPool.GetPooledObject();

        if (damageText == null)
            return;

        Color color = Color.white;
        if (e.ElementsInfoSO != null)
        {
            color = e.ElementsInfoSO.ColorText;
        }

        damageText.SetDamageTextValue(e.DamageText, e.WorldPosition, color);
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
        Transform canvasTransform = GetCanvasGO();
        int AmountToPool = 25;

        GameObject Parent = Instantiate(DamageTextHandlerParentPrefab, canvasTransform);
        ObjectPool = new ObjectPool<DamageText>(DamageTextPrefab, Parent.transform, AmountToPool);
    }
    private Transform GetCanvasGO()
    {
        GameObject go = GameObject.FindGameObjectWithTag("MainCanvas");
        if (go == null)
            return null;

        return go.transform;
    }

    public static void CallOnDamageTextInvoke(object sender, DamageInfo DamageInfo)
    {
        OnDamageTextSend?.Invoke(sender, DamageInfo);
    }

    private void OnDestroy()
    {
        OnDamageTextSend -= DamageManager_OnDamageHit;
    }
}