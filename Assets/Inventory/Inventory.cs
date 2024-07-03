using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{
    public int mora { get; private set; }
    public List<Item> itemList { get; private set; }

    public delegate void OnItemChanged(Item item);
    public event OnItemChanged OnItemAdd, OnItemRemove;
    public event Action<int> OnMoraChanged;

    public void AddMora(int Amt)
    {
        mora += Amt;
        mora = Mathf.Clamp(mora, 0, 100000000);
        OnMoraChanged?.Invoke(mora);
    }

    private Item GetItem(Item item)
    {
        if (item == null)
            return null;

        foreach(var itemRef in itemList)
        {
            if (itemRef.iItem == item.iItem)
                return itemRef;
        }
        return null;
    }

    public void AddItem(Item item)
    {
        if (item == null)
            return;

        Item existedItem = GetItem(item);

        if (existedItem != null)
        {
            ConsumableItem consumableItem = existedItem as ConsumableItem;

            if (consumableItem != null)
            {
                consumableItem.AddAmount(1);
                return;
            }
        }

        itemList.Add(item);
        OnItemAdd?.Invoke(item);
    }

    public void RemoveItem(Item item)
    {
        if (item == null)
            return;

        itemList.Remove(item);
        OnItemRemove?.Invoke(item);
    }

    public Inventory(int StartingMora = 0)
    {
        mora = StartingMora;
        itemList = new();
    }
}
