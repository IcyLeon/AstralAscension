using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OwnedCharactersIconManager : MonoBehaviour
{
    [SerializeField] private GameObject CharacterIconPrefab;
    [SerializeField] private Transform ParentTransform;
    private Dictionary<CharactersSO, OwnedCharacterIconButton> characterSOIcons = new();
    public OwnedCharacterIconButton selectedIcon { get; private set; }
    private CharacterStorage characterStorage;

    private void Awake()
    {
        CharacterScreenMiscEvent.OnSceneLoad += CharacterScreenMiscEvent_OnCharacterSceneLoad;
    }

    private void CharacterScreenMiscEvent_OnCharacterSceneLoad(CharacterStorage CharacterStorage)
    {
        characterStorage = CharacterStorage;

        if (characterStorage != null)
        {
            characterStorage.OnCharacterAdd += CharacterStorage_OnCharacterAdd;
            characterStorage.OnCharacterRemove += CharacterStorage_OnCharacterRemove;
            UpdateVisual();
        }
    }


    private void UpdateVisual()
    {
        if (characterSOIcons == null)
            return;

        for (int i = characterSOIcons.Count - 1; i >= 0; i--)
        {
            CharacterStorage_OnCharacterRemove(characterSOIcons.ElementAt(i).Key);
        }

        characterSOIcons.Clear();

        foreach (var characterSOKeyPair in characterStorage.characterStatList)
        {
            CharacterStorage_OnCharacterAdd(characterSOKeyPair.Key);
        }
    }

    private void CharacterStorage_OnCharacterRemove(CharactersSO CharactersSO)
    {
        if (!characterSOIcons.TryGetValue(CharactersSO, out OwnedCharacterIconButton OwnedCharacterIconButton))
            return;

        OwnedCharacterIconButton.OnCharacterIconSelected -= OwnedCharacterIconButton_OnCharacterIconSelected;
        Destroy(OwnedCharacterIconButton.gameObject);
        characterSOIcons.Remove(CharactersSO);
    }

    private void CharacterStorage_OnCharacterAdd(CharactersSO CharactersSO)
    {
        if (characterSOIcons.ContainsKey(CharactersSO))
            return;

        OwnedCharacterIconButton OwnedCharacterIconButton = Instantiate(CharacterIconPrefab, ParentTransform).GetComponent<OwnedCharacterIconButton>();
        OwnedCharacterIconButton.SetCharacterEquipmentManager(characterStorage.GetCharacterDataStat(CharactersSO).characterEquipmentManager);
        OwnedCharacterIconButton.OnCharacterIconSelected += OwnedCharacterIconButton_OnCharacterIconSelected;
        characterSOIcons.Add(CharactersSO, OwnedCharacterIconButton);
    }

    private void OwnedCharacterIconButton_OnCharacterIconSelected(OwnedCharacterIconButton OwnedCharacterIconButton)
    {
        if (selectedIcon == OwnedCharacterIconButton)
            return;

        selectedIcon = OwnedCharacterIconButton;
        OwnedCharacterButtonMiscEvent.Select(selectedIcon.characterEquipmentManager);
    }

    private void DestroyAllButton()
    {
        for(int i = characterSOIcons.Count - 1; i >= 0;i--)
        {
            CharacterStorage_OnCharacterRemove(characterSOIcons.ElementAt(i).Key);
        }

        characterSOIcons.Clear();
    }

    private void OnDestroy()
    {
        DestroyAllButton();

        CharacterScreenMiscEvent.OnSceneLoad -= CharacterScreenMiscEvent_OnCharacterSceneLoad;

        if (characterStorage != null)
        {
            characterStorage.OnCharacterAdd -= CharacterStorage_OnCharacterAdd;
            characterStorage.OnCharacterRemove -= CharacterStorage_OnCharacterRemove;
        }
    }
}
