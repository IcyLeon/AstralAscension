using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterDisplay : MonoBehaviour
{
    [SerializeField] private GameObject ModelsHandlerPrefab;
    private CharacterStorage characterStorage;
    private Dictionary<CharactersSO, ModelsHandler> characterObjectsDic = new();
    private OutfitDisplayController outfitDisplayController;

    // Start is called before the first frame update
    private void Awake()
    {
        CharacterScreenMiscEvent.OnSceneLoad += CharacterScreenMiscEvent_OnCharacterSceneLoad;
        outfitDisplayController = new();
    }

    private void CharacterScreenMiscEvent_OnCharacterSceneLoad(CharacterStorage CharacterStorage)
    {
        ResetList();
        characterStorage = CharacterStorage;

        if (characterStorage != null)
        {
            characterStorage.OnCharacterAdd += CharacterStorage_OnCharacterAdd;

            foreach (var characterStat in characterStorage.characterStatList)
                CharacterStorage_OnCharacterAdd(characterStat.Key);
        }
    }

    private void UnsubscribeStorageEvents()
    {
        if (characterStorage == null)
            return;

        characterStorage.OnCharacterAdd -= CharacterStorage_OnCharacterAdd;
    }

    private void OnEnable()
    {
        outfitDisplayController.OnEnable();
        OwnedCharacterButtonMiscEvent.OnCharacterIconSelected += OwnedCharacterButtonMiscEvent_OnCharacterIconSelected;
    }

    private void OnDisable()
    {
        outfitDisplayController.OnDisable();
        OwnedCharacterButtonMiscEvent.OnCharacterIconSelected -= OwnedCharacterButtonMiscEvent_OnCharacterIconSelected;
    }

    private void OwnedCharacterButtonMiscEvent_OnCharacterIconSelected(CharacterEquipmentManager CharacterEquipmentManager)
    {
        SwitchCharacter(CharacterEquipmentManager.charactersSO);
    }

    private void CharacterStorage_OnCharacterAdd(CharactersSO CharactersSO)
    {
        ModelsHandler modelsHandler = Instantiate(ModelsHandlerPrefab, transform).GetComponent<ModelsHandler>();
        characterObjectsDic.Add(CharactersSO, modelsHandler);
    }

    private void SwitchCharacter(CharactersSO CharactersSO)
    {
        if (!characterObjectsDic.ContainsKey(CharactersSO))
            return;

        ModelsHandler modelsHandler = characterObjectsDic[CharactersSO];
        CharacterDisplayMiscEvent.Show(CharactersSO, modelsHandler);

        foreach (var CharacterSOKey in characterObjectsDic.Keys)
            characterObjectsDic[CharacterSOKey].gameObject.SetActive(CharacterSOKey == CharactersSO);

    }

    private void ResetList()
    {
        if (characterStorage == null)
            return;

        UnsubscribeStorageEvents();
        foreach (var characterObject in characterObjectsDic)
            Destroy(characterObject.Value.gameObject);

        characterObjectsDic.Clear();

    }


    private void OnDestroy()
    {
        CharacterScreenMiscEvent.OnSceneLoad -= CharacterScreenMiscEvent_OnCharacterSceneLoad;
        UnsubscribeStorageEvents();
        outfitDisplayController.OnDestroy();
    }
}
