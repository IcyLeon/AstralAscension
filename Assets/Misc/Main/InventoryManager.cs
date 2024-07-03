using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[DisallowMultipleComponent]
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance { get; private set; }
    private Inventory inventory;

    public delegate void OnInventoryChanged(Inventory inventory);
    public static event OnInventoryChanged OnInventoryOld, OnInventoryNew;

    [field: SerializeField] public ItemManagerSO ItemManagerSO { get; private set; }

    [SerializeField] ArtifactSO artifactSOtest;
    [SerializeField] ArtifactSO artifactSOtest2;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetInventory(new Inventory(1000));


        inventory.AddItem(new Artifact(Rarity.FIVE_STAR, artifactSOtest2));
        inventory.AddItem(new Artifact(Rarity.FIVE_STAR, artifactSOtest));
        inventory.AddItem(new Artifact(Rarity.THREE_STAR, artifactSOtest));
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
