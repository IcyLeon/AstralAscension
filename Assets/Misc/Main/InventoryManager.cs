using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance { get; private set; }
    private Inventory inventory;

    public delegate void OnItemChanged(Item item);
    public static OnItemChanged OnItemAdd, OnItemRemove;
    public static Action<int> OnMoraChanged;

    private void Awake()
    {
        inventory = new Inventory(1000);
        instance = this;
    }

    public void AddMora(int Amt)
    {
        inventory.AddMora(Amt);
        OnMoraChanged?.Invoke(inventory.mora);
    }

    public void AddItem(Item item)
    {
        if (item == null)
            return;

        inventory.AddItem(item);
        OnItemAdd?.Invoke(item);
    }

    public void RemoveItem(Item item)
    {
        if (item == null)
            return;

        inventory.RemoveItem(item);
        OnItemRemove?.Invoke(item);
    }
}
