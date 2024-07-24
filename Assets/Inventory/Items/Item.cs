using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : IEntity
{
    public bool newStatus { get; private set; }
    public event EventHandler OnItemChanged;
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
        OnItemChanged?.Invoke(this, EventArgs.Empty);
    }

    public string GetItemName()
    {
        return GetInterfaceItemReference().GetItemName();
    }

    public Sprite GetItemIcon()
    {
        return GetInterfaceItemReference().GetItemIcon();
    }

    public string GetItemDescription()
    {
        return GetInterfaceItemReference().GetItemDescription();
    }

    public virtual Rarity GetItemRarity()
    {
        return GetInterfaceItemReference().GetItemRarity();
    }

    public ItemTypeSO GetItemType()
    {
        return GetInterfaceItemReference().GetItemType();
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
