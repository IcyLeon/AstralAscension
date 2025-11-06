using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[DisallowMultipleComponent]
public class DamageManager : MonoBehaviour
{
    public static DamageManager instance { get; private set; }
    [SerializeField] private int AmountToPool = 5;
    [SerializeField] private GameObject DamageTextPrefab;
    private ObjectPool<DamageText> ObjectPool;

    private void Awake()
    {
        instance = this;
        ObjectPool = new ObjectPool<DamageText>(DamageTextPrefab, transform, AmountToPool);
    }

    private void OnEnable()
    {
        DamageMiscEvent.OnDamageTextSend += DamageManager_OnDamageHit;
    }

    private void OnDisable()
    {
        DamageMiscEvent.OnDamageTextSend -= DamageManager_OnDamageHit;
    }

    private void DamageManager_OnDamageHit(DamageInfo e)
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
}