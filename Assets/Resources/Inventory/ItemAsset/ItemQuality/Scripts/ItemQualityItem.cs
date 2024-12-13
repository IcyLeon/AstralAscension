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

        LockItem.SetIItem(ItemQuality.iItem);
        EquipIcon.SetIItem(ItemQuality.iItem);

        item = ItemQuality.iItem as Item;
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
                return "+" + upgradableItems.GetLevel().ToString();
            default:
                return item.amount.ToString();
        }
    }
}
