using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : IEntity
{
    public bool newStatus { get; private set; }
    public event EventHandler<IEntityEvents> OnIEntityChanged;

    public IItem iItem { get; private set; }
    public int amount { get; protected set; }

    public Item(IItem iItem)
    {
        newStatus = true;
        amount = 0;
        this.iItem = iItem;
    }

    public void SetNewStatus(bool status)
    {
        newStatus = status;
        CallOnItemChanged();
    }

    public virtual void AddAmount(int amount)
    {
        this.amount += amount;
        CallOnItemChanged();
    }

    public abstract bool IsStackable();

    protected void CallOnItemChanged()
    {
        OnIEntityChanged?.Invoke(this, new IEntityEvents
        {
            iEntity = this,
        });
    }

    public string GetName()
    {
        return GetInterfaceItemReference().GetName();
    }

    public Sprite GetIcon()
    {
        return GetInterfaceItemReference().GetIcon();
    }

    public string GetDescription()
    {
        return GetInterfaceItemReference().GetDescription();
    }

    public virtual Rarity GetRarity()
    {
        return GetInterfaceItemReference().GetRarity();
    }

    public ItemTypeSO GetTypeSO()
    {
        return GetInterfaceItemReference().GetTypeSO();
    }

    public IItem GetInterfaceItemReference()
    {
        return iItem.GetInterfaceItemReference();
    }
    public bool IsNew()
    {
        return newStatus;
    }
}
