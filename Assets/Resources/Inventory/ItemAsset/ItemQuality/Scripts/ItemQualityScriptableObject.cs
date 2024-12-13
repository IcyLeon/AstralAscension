using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemQualityScriptableObject : ItemQualityButton
{
    [SerializeField] private ScriptableObject ItemScriptableObject;


    protected override void Awake()
    {
        base.Awake();
        SetIItem(ItemScriptableObject as IItem);
    }
}
