using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemQualityScriptableObject : ItemQuality
{
    [SerializeField] private ScriptableObject ItemScriptableObject;


    protected override void Awake()
    {
        base.Awake();
        SetIItem(ItemScriptableObject as IData);
    }
}
