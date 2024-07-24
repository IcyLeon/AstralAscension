using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemQualityIEntity : ItemQualityButton
{
    [field: SerializeField] public ItemQualitySelection ItemQualitySelection { get; private set; }
    public IEntity iEntity { get; private set; }
    public virtual void SetIEntity(IEntity entity)
    {
        iEntity = entity;

        if (iEntity == null)
            return;

        SetInterfaceItem(iEntity);
    }
    protected abstract void UpdateVisual();
}
