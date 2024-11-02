using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemQualityItem : ItemQualityIEntity
{
    [SerializeField] private LockItem LockItem;
    [SerializeField] private EquipIcon EquipIcon;
    private Item item;

    protected override void InitIEntity()
    {
        base.InitIEntity();

        LockItem.SetIItem(iItem);
        EquipIcon.SetIItem(iItem);

        item = iItem as Item;
    }

    protected override void UpdateVisual()
    {
        base.UpdateVisual();

        if (item == null)
            return;

        UpdateDisplayText(DisplayAmountSymbol() + item.amount);
    }

    private string DisplayAmountSymbol()
    {
        switch(item)
        {
            case UpgradableItems upgradableItems:
                return "+";
            default:
                return "";
        }
    }
}
