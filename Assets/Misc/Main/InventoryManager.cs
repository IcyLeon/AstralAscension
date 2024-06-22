using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance { get; private set; }
    private Inventory inventory;

    public delegate void OnInventoryChanged(Inventory inventory);
    public static event OnInventoryChanged OnInventoryOld, OnInventoryNew;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetInventory(new Inventory(1000));
    }

    private void SetInventory(Inventory inv)
    {
        if (inventory != null)
        {
            OnInventoryOld?.Invoke(inventory);
        }
        inventory = inv;
        OnInventoryNew?.Invoke(inventory);
    }

    private void OnDestroy()
    {
        if (inventory != null)
        {
            OnInventoryOld?.Invoke(inventory);
        }
    }

}
