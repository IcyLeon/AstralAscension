using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : IEntity
{
    public bool newStatus { get; private set; }
    public event Action<IEntityEvents> OnIEntityChanged;

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
        OnIEntityChanged?.Invoke(new IEntityEvents
        {
            iEntity = this,
        });
    }

    public virtual Rarity GetRarity()
    {
        return GetIItem().GetRarity();
    }

    public IItem GetIItem()
    {
        return iItem.GetIItem();
    }

    public bool IsNew()
    {
        return newStatus;
    }

    public string GetName()
    {
        return GetIItem().GetName();
    }

    public ItemTypeSO GetTypeSO()
    {
        return GetIItem().GetTypeSO();
    }

    public Sprite GetIcon()
    {
        return GetIItem().GetIcon();
    }

    public string GetDescription()
    {
        return GetIItem().GetDescription();
    }
}
