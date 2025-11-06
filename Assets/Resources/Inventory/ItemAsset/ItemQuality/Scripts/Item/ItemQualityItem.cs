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

        LockItem.SetIItem(iData);
        EquipIcon.SetIItem(iData);

        item = iData as Item;
    }

    protected override void UpdateVisual()
    {
        base.UpdateVisual();

        if (item == null)
            return;

        UpdateDisplayText(GetDisplayText());
    }

    private string GetDisplayText()
    {
        switch (item)
        {
            case UpgradableItems upgradableItems:
                return "+" + upgradableItems.level;
            default:
                return item.amount.ToString();
        }
    }
}
