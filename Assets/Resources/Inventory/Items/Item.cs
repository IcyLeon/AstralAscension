using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : IEntity
{
    public bool newStatus { get; private set; }
    public event Action<IEntity> OnIEntityChanged;

    public IData iData { get; private set; }
    public int amount { get; protected set; }

    public Item(IData IData)
    {
        newStatus = true;
        amount = 1;
        iData = IData;
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
        return iData.GetRaritySO();
    }

    public bool IsNew()
    {
        return newStatus;
    }

    public string GetName()
    {
        return iData.GetName();
    }

    public Sprite GetIcon()
    {
        return iData.GetIcon();
    }

    public string GetDescription()
    {
        return iData.GetDescription();
    }

    public ItemTypeSO GetTypeSO()
    {
        return iData.GetTypeSO();
    }
}
