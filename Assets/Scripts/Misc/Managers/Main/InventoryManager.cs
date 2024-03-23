using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventoryManager instance { get; private set; }
    private Inventory Inventory;

    public static Action<int> OnMoraChanged;

    // Start is called before the first frame update
    void Awake()
    {
        Inventory = new Inventory();
        instance = this;
    }

    public void AddMora(int Amt)
    {
        Inventory.AddMora(Amt);
        OnMoraChanged?.Invoke(Inventory.mora);
    }
}
