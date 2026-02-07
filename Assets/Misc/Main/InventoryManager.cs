using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DisallowMultipleComponent]
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance { get; private set; }
    public Inventory inventory { get; private set; }
    public static event Action<Inventory> OnInventoryChanged;

    private CharacterStorage characterStorage;

    [SerializeField] ArtifactSO artifactSOtest;
    [SerializeField] ArtifactSO artifactSOtest2;
    [SerializeField] ArtifactSO artifactSOtest3;
    [SerializeField] ArtifactSO artifactSOtest4;
    [SerializeField] ArtifactSO artifactSOtest5;
    [SerializeField] ArtifactSO artifactSOtest6;
    private void Awake()
    {
        instance = this;
        CharacterManager.OnCharacterStorageChanged += CharacterManager_OnCharacterStorageNew;

        SetInventory(new Inventory(1000));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            inventory.AddItem(artifactSOtest.CreateItem());
            inventory.AddItem(artifactSOtest2.CreateItem());
            inventory.AddItem(artifactSOtest3.CreateItem());
            inventory.AddItem(artifactSOtest4.CreateItem());
            inventory.AddItem(artifactSOtest5.CreateItem());
            inventory.AddItem(artifactSOtest6.CreateItem());
            inventory.AddItem(artifactSOtest2.CreateItem());
            inventory.AddItem(artifactSOtest3.CreateItem());
            inventory.AddItem(artifactSOtest4.CreateItem());
        }
    }

    private void SetInventory(Inventory inv)
    {
        inventory = inv;
        OnInventoryChanged?.Invoke(inventory);
    }

    private void CharacterManager_OnCharacterStorageNew(CharacterStorage CharacterStorage)
    {
        characterStorage = CharacterStorage;
    }

    public void UnequipItem(CharactersSO characterSO, UpgradableItems UpgradableItems)
    {
        if (UpgradableItems == null || characterSO == null)
            return;

        CharacterEquipmentManager characterEquipmentManager = characterStorage.GetCharacterDataStat(characterSO).characterEquipmentManager;
        characterEquipmentManager.UnequipItem(UpgradableItems);
    }

    public void EquipItem(CharactersSO characterSO, UpgradableItems upgradableItems)
    {
        if (upgradableItems == null || characterSO == null || characterStorage == null)
            return;

        CharacterEquipmentManager characterEquipmentManager = characterStorage.GetCharacterDataStat(characterSO).characterEquipmentManager;

        // get the existing artifact equipped from characterSO
        UpgradableItems currentItemEquipped = characterEquipmentManager.GetExistingItem(upgradableItems) as UpgradableItems;
        CharactersSO previousOwnerSO = upgradableItems.equipByCharacter;

        UnequipItem(previousOwnerSO, upgradableItems); // remove previous owner of the artifact
        UnequipItem(characterSO, currentItemEquipped);

        characterEquipmentManager.EquipItem(upgradableItems); // set the new owner of the artifact
        
        EquipItem(previousOwnerSO, currentItemEquipped); // set the previous owner to the artifact equipped from characterSO 
    }

    private void OnDestroy()
    {
        AccountCharacters.OnCharacterStorageChanged -= CharacterManager_OnCharacterStorageNew;
    }
}
