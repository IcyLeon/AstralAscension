using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class StatsHighlightContent
{
    [field: SerializeField] public Color32 SuccessColor { get; private set; }
    [field: SerializeField] public Color32 DefaultColor { get; private set; }
}

[DisallowMultipleComponent]
[RequireComponent(typeof(ArtifactPieceSetsDisplayManager))]
public class ArtifactPieceHighlightManager : MonoBehaviour
{
    [SerializeField] private ItemContentDisplay ItemContentDisplay;
    [SerializeField] private StatsHighlightContent StatsHighlightContent;
    private CharacterScreenPanel characterScreenPanel;
    private CharacterEquipmentManager selectedCharacterEquipmentManager;
    private ArtifactPieceSetsDisplayManager ArtifactPieceSetsDisplayManager;

    private Artifact artifact;

    private void Awake()
    {
        ItemContentDisplay.OnItemContentDisplayChanged += ItemContentDisplay_OnItemContentDisplayChanged;
        ArtifactPieceSetsDisplayManager = GetComponent<ArtifactPieceSetsDisplayManager>();
        characterScreenPanel = GetComponentInParent<CharacterScreenPanel>();
        characterScreenPanel.OnCharacterIconSelected += CharacterScreenPanel_OnIconSelected;
        UpdateOnCharacterSelected();
    }

    private void CharacterScreenPanel_OnIconSelected()
    {
        UpdateOnCharacterSelected();
    }

    private void UpdateOnCharacterSelected()
    {
        selectedCharacterEquipmentManager = characterScreenPanel.characterEquipmentManager;
    }

    private void ItemContentDisplay_OnItemContentDisplayChanged()
    {
        UnsubscribeEvents();
        artifact = ItemContentDisplay.iData as Artifact;
        SubscribeEvents();
    }


    private void SubscribeEvents()
    {
        if (artifact == null)
            return;

        artifact.OnIEntityChanged += IEntity_OnIEntityChanged;
        UpdateVisuals();
    }

    private void IEntity_OnIEntityChanged(IEntity e)
    {
        UpdateVisuals();
    }

    private void ResetAll()
    {
        for (int i = 0; i < ArtifactPieceSetsDisplayManager.GetTotalPieceInfo(); i++)
        {
            ArtifactPieceSetsDisplayManager.GetArtifactPieceInfoDisplay(i).SetTextColor(StatsHighlightContent.DefaultColor);
        }
    }

    private void UpdateVisuals()
    {
        ResetAll();

        if (selectedCharacterEquipmentManager == null)
            return;

        int index = GetArtifactBuffCurrentIndex(selectedCharacterEquipmentManager.effectManager);

        for (int i = 0; i <= index; i++)
        {
            ArtifactPieceSetsDisplayManager.GetArtifactPieceInfoDisplay(i).SetTextColor(StatsHighlightContent.SuccessColor);
        }
    }

    private int GetArtifactBuffCurrentIndex(EffectManager effectManager)
    {
        if (artifact == null)
            return -1;

        return effectManager.GetArtifactBuffCurrentIndex(artifact.artifactSO.ArtifactFamilySO);
    }

    private void UnsubscribeEvents()
    {
        if (artifact == null)
            return;

        artifact.OnIEntityChanged -= IEntity_OnIEntityChanged;
    }

    private void OnDestroy()
    {
        characterScreenPanel.OnCharacterIconSelected -= CharacterScreenPanel_OnIconSelected;
        ItemContentDisplay.OnItemContentDisplayChanged -= ItemContentDisplay_OnItemContentDisplayChanged;
    }
}
