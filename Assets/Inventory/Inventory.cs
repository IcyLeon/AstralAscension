using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{
    public int mora { get; private set; }
    public List<IEntity> itemList { get; private set; }

    public delegate void OnItemChanged(IEntity item);
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

    private IEntity GetItem(IEntity iEntity)
    {
        if (iEntity == null)
            return null;

        foreach(var itemRef in itemList)
        {
            if (itemRef.GetIItem() == iEntity)
                return itemRef;
        }
        return null;
    }

    public void AddItem(IEntity iEntity)
    {
        if (iEntity == null)
            return;

        Item existedItem = GetItem(iEntity) as Item;

        if (existedItem != null && existedItem.IsStackable())
        {
            existedItem.AddAmount(1);
            return;
        }

        itemList.Add(iEntity);
        OnItemAdd?.Invoke(iEntity);
    }

    public void RemoveItem(IEntity iItem)
    {
        if (iItem == null)
            return;

        itemList.Remove(iItem);
        OnItemRemove?.Invoke(iItem);
    }

    public Inventory(int StartingMora = 0)
    {
        mora = StartingMora;
        itemList = new();
    }
}
