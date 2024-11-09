using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance { get; private set; }
    private AssetManager assetManager;

    public Inventory inventory { get; private set; }

    public delegate void OnInventoryChanged(Inventory inventory);
    public static event OnInventoryChanged OnInventoryOld, OnInventoryNew;

    private CharacterStorage characterStorage;

    [SerializeField] ArtifactSO artifactSOtest;
    [SerializeField] ArtifactSO artifactSOtest2;
    [SerializeField] ArtifactSO artifactSOtest3;
    [SerializeField] ArtifactSO artifactSOtest4;
    [SerializeField] ArtifactSO artifactSOtest5;
    private void Awake()
    {
        instance = this;
        CharacterManager.OnCharacterStorageOld += CharacterManager_OnCharacterStorageOld;
        CharacterManager.OnCharacterStorageNew += CharacterManager_OnCharacterStorageNew;
    }

    private void Start()
    {
        assetManager = AssetManager.instance;
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
            inventory.AddItem(new Artifact(Rarity.FOUR_STAR, artifactSOtest2));
            inventory.AddItem(new Artifact(Rarity.FOUR_STAR, artifactSOtest3));
            inventory.AddItem(new Artifact(Rarity.FOUR_STAR, artifactSOtest4));
            inventory.AddItem(new Artifact(Rarity.THREE_STAR, artifactSOtest5));
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

        if (characterStorage == null || !characterStorage.playableCharacterStatList.ContainsKey(characterSO))
            return;

        PlayableCharacterDataStat playableCharacter = characterStorage.playableCharacterStatList[characterSO];
        playableCharacter.UnequipItem(UpgradableItems.GetIItem().GetTypeSO());
    }

    public void EquipItem(CharactersSO characterSO, UpgradableItems upgradableItems)
    {
        if (upgradableItems == null || characterSO == null)
            return;

        if (characterStorage == null || !characterStorage.playableCharacterStatList.ContainsKey(characterSO))
            return;

        PlayableCharacterDataStat playableCharacter = characterStorage.playableCharacterStatList[characterSO];

        // get the existing artifact equipped from characterSO
        UpgradableItems currentItemEquipped = playableCharacter.GetItem(upgradableItems.GetIItem().GetTypeSO()) as UpgradableItems;

        CharactersSO previousOwnerSO = upgradableItems.equipByCharacter;

        UnequipItem(previousOwnerSO, upgradableItems); // remove previous owner of the artifact
        UnequipItem(characterSO, currentItemEquipped);

        playableCharacter.EquipItem(upgradableItems); // set the new owner of the artifact

        EquipItem(previousOwnerSO, currentItemEquipped); // set the previous owner to the artifact equipped from characterSO 
    }

    private void OnDestroy()
    {
        CharacterManager.OnCharacterStorageOld -= CharacterManager_OnCharacterStorageOld;
        CharacterManager.OnCharacterStorageNew -= CharacterManager_OnCharacterStorageNew;
    }
}
