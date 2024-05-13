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

    private void Awake()
    {
        DamageChanged += DamageManager_ElementsChanged;
        ElementalReactionChanged += DamageManager_ElementalReactionChanged;
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

    private void DamageManager_ElementsChanged(object sender, ElementsInfo e)
    {
        IDamageable target = sender as IDamageable;

        if (instance.isImmune(target, e.elementsSO))
        {
            DamageText damageText = ObjectPool.GetPooledObject();

            if (damageText == null)
                return;

            damageText.SetDamageTextValue(instance.ImmuneSO.DisplayElementalReactionText, e.WorldPosition, instance.ImmuneSO.ColorText);
            return;
        }

        for (int i = 0; i < e.DamageText.Count; i++)
        {
            DamageText damageText = ObjectPool.GetPooledObject();

            if (damageText == null)
                return;

            Color color = Color.white;
            if (e.elementsSO != null)
            {
                color = e.elementsSO.ColorText;
            }

            damageText.SetDamageTextValue(e.DamageText[i], e.WorldPosition, color);
        }
    }

    private void DamageManager_ElementalReactionChanged(object sender, ElementalReactionInfo e)
    {
        for (int i = 0; i < e.DamageText.Count; i++)
        {
            DamageText damageText = ObjectPool.GetPooledObject();

            if (damageText == null)
                return;

            damageText.SetDamageTextValue(e.DamageText[i], e.WorldPosition, e.elementalReactionSO.ColorText);
        }
    }

    private void OnDestroy()
    {
        DamageChanged -= DamageManager_ElementsChanged;
        ElementalReactionChanged -= DamageManager_ElementalReactionChanged;
    }
}
