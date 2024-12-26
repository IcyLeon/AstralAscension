using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArtifactDisplay : MonoBehaviour
{
    private CharacterSelection characterSelection;
    private CharacterStorage characterStorage;
    private ArtifactSphereManager artifactSphereManager;
    public CharactersSO currentCharacterSelected { get; private set; }

    private void Awake()
    {
        characterSelection = GetComponentInParent<CharacterSelection>();
        characterSelection.OnCharacterSelected += CharacterSelection_OnCharacterSelected;
        CharacterManager.OnCharacterStorageNew += CharacterManager_OnCharacterStorageNew;
        artifactSphereManager = GetComponentInChildren<ArtifactSphereManager>();
    }

    private void CharacterSelection_OnCharacterSelected()
    {
        currentCharacterSelected = characterSelection.currentCharacterSelected;
        InitArtifactInventory();
    }

    private void CharacterManager_OnCharacterStorageNew(CharacterStorage CharacterStorage)
    {
        characterStorage = CharacterStorage;
    }

    private void InitArtifactInventory()
    {
        CharacterDataStat characterDataStat = characterStorage.GetCharacterDataStat(currentCharacterSelected);

        if (characterDataStat == null)
            return;

        artifactSphereManager.SetArtifactInventory(characterDataStat.characterInventory.artifactInventory);
    }


    private void OnDestroy()
    {
        characterSelection.OnCharacterSelected -= CharacterSelection_OnCharacterSelected;
        CharacterManager.OnCharacterStorageNew -= CharacterManager_OnCharacterStorageNew;
    }
}
