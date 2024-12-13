using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.TextCore.Text;

[DisallowMultipleComponent]
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance { get; private set; }

    public Inventory inventory { get; private set; }

    public delegate void OnInventoryChanged(Inventory inventory);
    public static event OnInventoryChanged OnInventoryOld, OnInventoryNew;

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
        CharacterManager.OnCharacterStorageOld += CharacterManager_OnCharacterStorageOld;
        CharacterManager.OnCharacterStorageNew += CharacterManager_OnCharacterStorageNew;

        SetInventory(new Inventory(1000));
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        if (characterStorage != null)
            return;

        CharacterManager_OnCharacterStorageNew(CharacterManager.instance.characterStorage);
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
        if (inventory != null)
        {
            OnInventoryOld?.Invoke(inventory);
        }
        inventory = inv;
        OnInventoryNew?.Invoke(inventory);
    }

    private void CharacterManager_OnCharacterStorageOld(CharacterStorage CharacterStorage)
    {
    }

    private void CharacterManager_OnCharacterStorageNew(CharacterStorage CharacterStorage)
    {
        characterStorage = CharacterStorage;
    }

    public void UnequipItem(CharactersSO characterSO, UpgradableItems UpgradableItems)
    {
        if (UpgradableItems == null || characterSO == null)
            return;

        if (characterStorage == null || !characterStorage.characterStatList.ContainsKey(characterSO))
            return;

        CharacterDataStat CharacterDataStat = characterStorage.characterStatList[characterSO];
        CharacterDataStat.UnequipItem(UpgradableItems.GetIItem().GetTypeSO());
    }

    public void EquipItem(CharactersSO characterSO, UpgradableItems upgradableItems)
    {
        if (upgradableItems == null || characterSO == null)
            return;

        if (characterStorage == null || !characterStorage.characterStatList.ContainsKey(characterSO))
            return;

        CharacterDataStat CharacterDataStat = characterStorage.characterStatList[characterSO];

        // get the existing artifact equipped from characterSO
        UpgradableItems currentItemEquipped = CharacterDataStat.GetItem(upgradableItems.GetTypeSO()) as UpgradableItems;

        CharactersSO previousOwnerSO = upgradableItems.equipByCharacter;

        UnequipItem(previousOwnerSO, upgradableItems); // remove previous owner of the artifact
        UnequipItem(characterSO, currentItemEquipped);

        CharacterDataStat.EquipItem(upgradableItems); // set the new owner of the artifact
        
        EquipItem(previousOwnerSO, currentItemEquipped); // set the previous owner to the artifact equipped from characterSO 
    }

    private void OnDestroy()
    {
        CharacterManager.OnCharacterStorageOld -= CharacterManager_OnCharacterStorageOld;
        CharacterManager.OnCharacterStorageNew -= CharacterManager_OnCharacterStorageNew;
    }
}
