using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArtifactDisplay : MonoBehaviour
{
    private ArtifactBubbleManager artifactBubbleManager;

    private void Awake()
    {
        InitArtifactBubbleManager();
    }

    private void OnEnable()
    {
        OwnedCharacterButtonMiscEvent.OnCharacterIconSelected += OwnedCharacterButtonMiscEvent_OnCharacterIconSelected;
    }

    private void OnDisable()
    {
        OwnedCharacterButtonMiscEvent.OnCharacterIconSelected -= OwnedCharacterButtonMiscEvent_OnCharacterIconSelected;
    }

    private void OwnedCharacterButtonMiscEvent_OnCharacterIconSelected(CharacterEquipmentManager CharacterEquipmentManager)
    {
        if (CharacterEquipmentManager == null)
            return;

        artifactBubbleManager.SetArtifactInventory(CharacterEquipmentManager.artifactEquipment);
    }

    private void InitArtifactBubbleManager()
    {
        if (artifactBubbleManager != null)
            return;

        artifactBubbleManager = GetComponentInChildren<ArtifactBubbleManager>(true);
    }

}
