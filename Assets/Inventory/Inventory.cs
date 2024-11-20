using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{
    public int mora { get; private set; }
    public Dictionary<IItem, IItem> itemList { get; private set; }

    public delegate void OnItemChanged(IItem item);
    public event OnItemChanged OnItemAdd, OnItemRemove;
    public event Action<int> OnMoraChanged;

    public void AddMora(int Amt)
    {
        mora += Amt;
        mora = Mathf.Clamp(mora, 0, 100000000);
        OnMoraChanged?.Invoke(mora);
    }

    private IItem GetItem(IItem IItem)
    {
        if (!itemList.TryGetValue(IItem, out var item))
            return null;

        return item;
    }

    public void AddItem(IItem IItem)
    {
        if (IItem == null)
            return;

        Item existedItem = GetItem(IItem) as Item;

        if (existedItem != null && existedItem.IsStackable())
        {
            existedItem.AddAmount(1);
            return;
        }

        itemList.Add(IItem, IItem);
        OnItemAdd?.Invoke(IItem);
    }

    public void RemoveItem(IEntity IEntity)
    {
        if (IEntity == null)
            return;

        itemList.Remove(IEntity);
        OnItemRemove?.Invoke(IEntity);
    }

    public Inventory(int StartingMora = 0)
    {
        mora = StartingMora;
        itemList = new();
    }
}
