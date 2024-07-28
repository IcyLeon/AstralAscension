using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance { get; private set; }
    public Inventory inventory { get; private set; }

    public delegate void OnInventoryChanged(Inventory inventory);
    public static event OnInventoryChanged OnInventoryOld, OnInventoryNew;

    [SerializeField] ArtifactSO artifactSOtest;
    [SerializeField] ArtifactSO artifactSOtest2;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetInventory(new Inventory(1000));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            inventory.AddItem(new Artifact(Rarity.FIVE_STAR, artifactSOtest));
            inventory.AddItem(new Artifact(Rarity.THREE_STAR, artifactSOtest));
            inventory.AddItem(new Artifact(Rarity.FOUR_STAR, artifactSOtest));
            inventory.AddItem(new Artifact(Rarity.FOUR_STAR, artifactSOtest));
        }
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
