using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemQualityScriptableObject : ItemQualityButton
{
    [SerializeField] private ScriptableObject ItemScriptableObject;

    public IItem iItem
    {
        get
        {
            return ItemScriptableObject as IItem;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        SetInterfaceItem(iItem);
    }
}
