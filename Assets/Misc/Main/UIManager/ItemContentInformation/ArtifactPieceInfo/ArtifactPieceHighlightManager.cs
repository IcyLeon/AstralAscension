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
    private CharacterDataStat selectedCharacterDataStat;
    private ArtifactPieceSetsDisplayManager ArtifactPieceSetsDisplayManager;

    private IItem iItem;

    private void Awake()
    {
        ItemContentDisplay.OnItemContentDisplayChanged += ItemContentDisplay_OnItemContentDisplayChanged;
        ArtifactPieceSetsDisplayManager = GetComponent<ArtifactPieceSetsDisplayManager>();
        characterScreenPanel = GetComponentInParent<CharacterScreenPanel>();
        characterScreenPanel.OnIconSelected += CharacterScreenPanel_OnIconSelected;
        UpdateOnCharacterSelected();
    }

    private void CharacterScreenPanel_OnIconSelected()
    {
        UpdateOnCharacterSelected();
    }

    private void UpdateOnCharacterSelected()
    {
        selectedCharacterDataStat = characterScreenPanel.currentCharacterSelected;
    }

    private void ItemContentDisplay_OnItemContentDisplayChanged()
    {
        UnsubscribeEvents();
        iItem = ItemContentDisplay.iItem;
        SubscribeEvents();
    }


    private void SubscribeEvents()
    {
        IEntity iEntity = iItem as IEntity;

        if (iEntity == null)
            return;

        iEntity.OnIEntityChanged += IEntity_OnIEntityChanged;
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

        if (selectedCharacterDataStat == null)
            return;

        int index = GetArtifactBuffCurrentIndex(selectedCharacterDataStat);

        for (int i = 0; i <= index; i++)
        {
            ArtifactPieceSetsDisplayManager.GetArtifactPieceInfoDisplay(i).SetTextColor(StatsHighlightContent.SuccessColor);
        }
    }

    private int GetArtifactBuffCurrentIndex(CharacterDataStat CharacterDataStat)
    {
        ArtifactSO artifactSO = iItem.GetIItem() as ArtifactSO;

        if (artifactSO == null)
            return -1;

        return CharacterDataStat.effectManager.GetArtifactBuffCurrentIndex(artifactSO.ArtifactFamilySO);
    }

    private void UnsubscribeEvents()
    {
        IEntity iEntity = iItem as IEntity;

        if (iEntity == null)
            return;

        iEntity.OnIEntityChanged -= IEntity_OnIEntityChanged;
    }

    private void OnDestroy()
    {
        characterScreenPanel.OnIconSelected -= CharacterScreenPanel_OnIconSelected;
        ItemContentDisplay.OnItemContentDisplayChanged -= ItemContentDisplay_OnItemContentDisplayChanged;
    }
}
