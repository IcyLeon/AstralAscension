using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{
    public int mora { get; private set; }
    private List<Item> itemList;

    public delegate void OnItemChanged(Item item);
    public event OnItemChanged OnItemAdd, OnItemRemove;
    public event Action<int> OnMoraChanged;

    public void AddMora(int Amt)
    {
        mora += Amt;
        mora = Mathf.Clamp(mora, 0, 100000000);
        OnMoraChanged?.Invoke(mora);
    }

    public void AddItem(Item item)
    {
        if (item == null)
            return;

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
