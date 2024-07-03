using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : IItem
{
    public event EventHandler OnItemChanged;
    public Rarity rarity { get; private set; }
    public IItem iItem { get; private set; }
    public int amount { get; protected set; }

    public Item(Rarity Rarity, IItem iItem)
    {
        rarity = Rarity;
        this.iItem = iItem;
    }

    protected void CallOnItemChanged()
    {
        OnItemChanged?.Invoke(this, EventArgs.Empty);
    }

    public string GetItemName()
    {
        return iItem.GetItemName();
    }

    public Sprite GetItemIcon()
    {
        return iItem.GetItemIcon();
    }

    public string GetItemDescription()
    {
        return iItem.GetItemDescription();
    }

    public Rarity GetItemRarity()
    {
        return rarity;
    }

    public string GetItemType()
    {
        return iItem.GetItemType();
    }
}
