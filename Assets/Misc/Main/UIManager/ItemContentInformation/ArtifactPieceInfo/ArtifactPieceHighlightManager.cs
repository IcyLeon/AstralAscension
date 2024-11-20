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

    [SerializeField] private CharactersSO charactersSO;

    private ArtifactPieceSetsDisplayManager ArtifactPieceSetsDisplayManager;

    private CharacterStorage characterStorage;

    private IItem iItem;

    private void Awake()
    {
        CharacterManager.OnCharacterStorageOld += CharacterManager_OnCharacterStorageOld;
        CharacterManager.OnCharacterStorageNew += CharacterManager_OnCharacterStorageNew;

        ItemContentDisplay.OnItemContentDisplayChanged += ItemContentDisplay_OnItemContentDisplayChanged;

        ArtifactPieceSetsDisplayManager = GetComponent<ArtifactPieceSetsDisplayManager>();
    }

    private void Start()
    {
        if (characterStorage != null)
            return;

        characterStorage = CharacterManager.instance.characterStorage;
        SetIItem(ItemContentDisplay.iItem);
    }

    private void ItemContentDisplay_OnItemContentDisplayChanged(object sender, ItemContentDisplay.ItemContentEvent e)
    {
        SetIItem(e.iItem);
    }

    private void SetIItem(IItem IItem)
    {
        UnsubscribeEvents();
        iItem = IItem;
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
        foreach (var ArtifactPieceInfoDisplay in ArtifactPieceSetsDisplayManager.ArtifactPieceInfoDisplayList)
        {
            ArtifactPieceInfoDisplay.SetTextColor(StatsHighlightContent.DefaultColor);
        }
    }

    private void UpdateVisuals()
    {
        ResetAll();

        if (characterStorage == null)
            return;

        PlayableCharacterDataStat c = characterStorage.HasObtainedCharacter(charactersSO) as PlayableCharacterDataStat;

        if (c == null)
            return;

        int eventCount = c.characterArtifactManager.GetPieceEventCount(iItem);

        for (int i = 0; i < eventCount; i++)
        {
            ArtifactPieceSetsDisplayManager.ArtifactPieceInfoDisplayList[i].SetTextColor(StatsHighlightContent.SuccessColor);
        }
    }

    private void UnsubscribeEvents()
    {
        IEntity iEntity = iItem as IEntity;

        if (iEntity == null)
            return;

        iEntity.OnIEntityChanged -= IEntity_OnIEntityChanged;
    }

    public void SetCharacterSO(CharactersSO charactersSO)
    {
        this.charactersSO = charactersSO;
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

        ItemContentDisplay.OnItemContentDisplayChanged -= ItemContentDisplay_OnItemContentDisplayChanged;
    }
}
