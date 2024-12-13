using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{
    public int mora { get; private set; }
    public Dictionary<Item, Item> itemList { get; private set; }

    public delegate void OnItemChanged(Item Item);
    public event OnItemChanged OnItemAdd, OnItemRemove;
    public event Action<int> OnMoraChanged;

    public void AddMora(int Amt)
    {
        mora += Amt;
        ClampMora();
        OnMoraChanged?.Invoke(mora);
    }

    public void RemoveMora(int Amt)
    {
        mora -= Amt;
        ClampMora();
        OnMoraChanged?.Invoke(mora);
    }

    private void ClampMora()
    {
        mora = Mathf.Clamp(mora, 0, 100000000);
    }

    private Item GetItem(Item Item)
    {
        if (!itemList.TryGetValue(Item, out var item))
            return null;

        return Item;
    }

    public void AddItem(Item Item)
    {
        if (Item == null)
            return;

        if (Item.IsStackable())
        {
            Item existedItem = GetItem(Item);
            if (existedItem != null)
            {
                existedItem.AddAmount(Item.amount);
                return;
            }
        }

        for (int i = 0; i < Item.amount; i++)
        {
            itemList.Add(Item, Item);
            OnItemAdd?.Invoke(Item);
        }
    }

    public void RemoveItem(Item Item)
    {
        if (Item == null)
            return;

        itemList.Remove(Item);
        OnItemRemove?.Invoke(Item);
    }

    public Inventory(int StartingMora = 0)
    {
        mora = StartingMora;
        itemList = new();
    }
}
