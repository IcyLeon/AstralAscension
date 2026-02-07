using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemQualityItem : ItemQualityIEntity
{
    [SerializeField] private LockItem LockItem;
    [SerializeField] private EquipIcon EquipIcon;
    public override void SetIData(ItemQualityIEntityDisplayData ItemQualityIEntityDisplayData)
    {
        base.SetIData(ItemQualityIEntityDisplayData);

        LockItem.SetIItem(iEntity);
        EquipIcon.SetIItem(iEntity);
    }
}
