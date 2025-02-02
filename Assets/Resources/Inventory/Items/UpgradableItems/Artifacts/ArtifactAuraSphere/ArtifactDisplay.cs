using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArtifactDisplay : MonoBehaviour
{
    private CharacterSelection characterSelection;
    private ArtifactBubbleManager artifactBubbleManager;
    public CharacterDataStat currentCharacterSelected { get; private set; }

    private void Awake()
    {
        characterSelection = GetComponentInParent<CharacterSelection>();
        characterSelection.OnCharacterSelected += CharacterSelection_OnCharacterSelected;
        artifactBubbleManager = GetComponentInChildren<ArtifactBubbleManager>();
        UpdateCurrentSelectedCharacter();
    }

    private void CharacterSelection_OnCharacterSelected()
    {
        UpdateCurrentSelectedCharacter();
    }

    private void UpdateCurrentSelectedCharacter()
    {
        currentCharacterSelected = characterSelection.currentCharacterSelected;
        InitArtifactInventory();
    }

    private void InitArtifactInventory()
    {
        if (currentCharacterSelected == null)
            return;

        artifactBubbleManager.SetArtifactInventory(currentCharacterSelected.characterInventory.artifactInventory);
    }


    private void OnDestroy()
    {
        characterSelection.OnCharacterSelected -= CharacterSelection_OnCharacterSelected;
    }
}
