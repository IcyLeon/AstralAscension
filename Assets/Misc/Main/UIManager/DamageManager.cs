using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static ElementalReactionsManager;

public class DamageManager : MonoBehaviour
{
    [SerializeField] private GameObject DamageTextPrefab;
    [SerializeField] private GameObject DamageTextHandlerParentPrefab;
    private ObjectPool<DamageText> ObjectPool;

    private void Awake()
    {
        ElementalReactionsManager.DamageChanged += DamageManager_ElementsChanged;
        ElementalReactionsManager.ElementalReactionChanged += DamageManager_ElementalReactionChanged;
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
        int AmountToPool = 25;

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
        GameObject go = GameObject.FindGameObjectWithTag("MainCanvas");
        if (go == null)
            return null;

        return go.GetComponent<Canvas>();
    }

    private void DamageManager_ElementsChanged(object sender, ElementsInfo e)
    {
        IDamageable target = sender as IDamageable;

        if (instance.isImmune(target, e.elementsSO))
        {
            DamageText damageText = ObjectPool.GetPooledObject();

            if (damageText == null)
                return;

            damageText.SetDamageTextValue("Immune", e.WorldPosition, instance.ImmuneColorText);
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
        ElementalReactionsManager.DamageChanged -= DamageManager_ElementsChanged;
        ElementalReactionsManager.ElementalReactionChanged -= DamageManager_ElementalReactionChanged;
    }
}
