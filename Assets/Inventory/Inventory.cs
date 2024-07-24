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

    public List<T> GetItemListOfType<T>()
    {
        List<T> tempList = new();

        foreach (var item in itemList)
        {
            if (item is T typedItem)
            {
                tempList.Add(typedItem);
            }
        }
        return tempList;
    }

    private Item GetItem(IItem iItem)
    {
        if (iItem == null)
            return null;

        foreach(var itemRef in itemList)
        {
            if (itemRef.iItem == iItem)
                return itemRef;
        }
        return null;
    }

    public void AddItem(Item item)
    {
        if (item == null)
            return;

        Item existedItem = GetItem(item.GetInterfaceItemReference());

        if (existedItem != null && existedItem.IsStackable())
        {
            existedItem.AddAmount(1);
            return;
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
