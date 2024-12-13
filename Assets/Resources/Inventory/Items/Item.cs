using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : IEntity
{
    public bool newStatus { get; private set; }
    public event Action<IEntity> OnIEntityChanged;

    public IItem iItem { get; private set; }
    public int amount { get; protected set; }

    public Item(IItem IItem)
    {
        newStatus = true;
        amount = 1;
        iItem = IItem;
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

    public virtual bool IsStackable()
    {
        return false;
    }

    protected void CallOnItemChanged()
    {
        OnIEntityChanged?.Invoke(this);
    }

    public ItemRaritySO GetRaritySO()
    {
        return GetIItem().GetRaritySO();
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
