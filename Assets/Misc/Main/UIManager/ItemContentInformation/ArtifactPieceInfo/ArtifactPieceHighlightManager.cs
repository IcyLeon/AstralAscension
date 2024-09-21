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

[RequireComponent(typeof(ArtifactPieceSetsDisplayManager))]
public class ArtifactPieceHighlightManager : MonoBehaviour
{
    [SerializeField] private ItemContentDisplay ItemContentDisplay;

    [SerializeField] private ArtifactPieceSetsDisplayManager ArtifactPieceSetsDisplayManager;

    [SerializeField] private StatsHighlightContent StatsHighlightContent;

    [SerializeField] private CharactersSO charactersSO;

    private CharacterStorage characterStorage;

    private IItem iItem;

    private void Awake()
    {
        CharacterManager.OnCharacterStorageOld += CharacterManager_OnCharacterStorageOld;
        CharacterManager.OnCharacterStorageNew += CharacterManager_OnCharacterStorageNew;

        ItemContentDisplay.OnItemContentDisplayChanged += ItemContentDisplay_OnItemContentDisplayChanged;
    }

    private void Start()
    {
        if (characterStorage != null)
            return;

        characterStorage = CharacterManager.instance.characterStorage;
    }

    private void ItemContentDisplay_OnItemContentDisplayChanged(object sender, ItemContentDisplay.ItemContentEvent e)
    {
        UnSubscribeUpgradableEvents();
        iItem = e.iItem;
        SubscribeUpgradableEvents();
    }

    private void SubscribeUpgradableEvents()
    {
        Item item = iItem as Item;

        if (item == null)
            return;

        item.OnItemChanged += UpgradableItems_OnItemChanged;
        UpdateVisual();
    }

    private void ResetAll()
    {
        foreach (var ArtifactPieceInfoDisplay in ArtifactPieceSetsDisplayManager.ArtifactPieceInfoDisplayList)
        {
            ArtifactPieceInfoDisplay.SetTextColor(StatsHighlightContent.DefaultColor);
        }
    }

    private void UpgradableItems_OnItemChanged(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        ResetAll();

        CharacterDataStat c = characterStorage.HasObtainedCharacter(charactersSO);

        if (c == null)
            return;

        int eventCount = c.artifactEffectManager.GetPieceEventCount(iItem);

        for (int i = 0; i < eventCount; i++)
        {
            ArtifactPieceSetsDisplayManager.ArtifactPieceInfoDisplayList[i].SetTextColor(StatsHighlightContent.SuccessColor);
        }
    }

    private void UnSubscribeUpgradableEvents()
    {
        Item item = iItem as Item;

        if (item == null)
            return;

        item.OnItemChanged -= UpgradableItems_OnItemChanged;
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

        UnSubscribeUpgradableEvents();
        ItemContentDisplay.OnItemContentDisplayChanged -= ItemContentDisplay_OnItemContentDisplayChanged;
    }
}
