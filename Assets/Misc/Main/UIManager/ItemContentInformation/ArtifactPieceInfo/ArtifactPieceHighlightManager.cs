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
    private CharactersSO selectedCharactersSO;
    private ArtifactPieceSetsDisplayManager ArtifactPieceSetsDisplayManager;

    private CharacterStorage characterStorage;

    private IItem iItem;

    private void Awake()
    {
        CharacterManager.OnCharacterStorageOld += CharacterManager_OnCharacterStorageOld;
        CharacterManager.OnCharacterStorageNew += CharacterManager_OnCharacterStorageNew;
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
        selectedCharactersSO = characterScreenPanel.currentCharacterSelected;
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

        if (characterStorage == null)
            return;

        CharacterDataStat c = characterStorage.HasObtainedCharacter(selectedCharactersSO);

        if (c == null)
            return;

        int index = GetArtifactBuffCurrentIndex(c);

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


    private void CharacterManager_OnCharacterStorageOld(CharacterStorage CharacterStorage)
    {
    }
    private void CharacterManager_OnCharacterStorageNew(CharacterStorage CharacterStorage)
    {
        characterStorage = CharacterStorage;
    }

    private void OnDestroy()
    {
        CharacterManager.OnCharacterStorageOld -= CharacterManager_OnCharacterStorageOld;
        CharacterManager.OnCharacterStorageNew -= CharacterManager_OnCharacterStorageNew;
        characterScreenPanel.OnIconSelected -= CharacterScreenPanel_OnIconSelected;
        ItemContentDisplay.OnItemContentDisplayChanged -= ItemContentDisplay_OnItemContentDisplayChanged;
    }
}
