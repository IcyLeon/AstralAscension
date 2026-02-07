using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{
    public int mora { get; private set; }
    public HashSet<Item> itemList { get; } = new();

    public delegate void OnItemChanged(Item Item);
    public event OnItemChanged OnItemAdd, OnItemRemove;
    public event Action OnMoraChanged;

    public void AddMora(int Amt)
    {
        mora += Amt;
        ClampMora();
        OnMoraChanged?.Invoke();
    }

    private void ClampMora()
    {
        mora = Mathf.Clamp(mora, 0, 10000000);
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

        itemList.Add(Item);
        OnItemAdd?.Invoke(Item);
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
    }
}
