using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterDisplay : MonoBehaviour
{
    private CharacterStorage characterStorage;
    private Dictionary<CharactersSO, GameObject> characterObjectsDic;
    public CharactersSO currentCharacterSelected { get; private set; }

    // Start is called before the first frame update
    private void Awake()
    {
        characterObjectsDic = new();

        CharacterScreenMiscEvent.OnCharacterSceneLoad += CharacterScreenMiscEvent_OnCharacterSceneLoad;
    }

    private void CharacterScreenMiscEvent_OnCharacterSceneLoad(CharacterStorage CharacterStorage)
    {
        characterStorage = CharacterStorage;

        if (characterStorage != null)
        {
            characterStorage.OnCharacterAdd += CharacterStorage_OnCharacterAdd;
            InitCharacterObjects();
        }
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

        SwitchCharacter(CharacterEquipmentManager.charactersSO);
    }

    private void CharacterStorage_OnCharacterAdd(CharactersSO CharactersSO)
    {
        characterObjectsDic.Add(CharactersSO, null);
    }

    private void SwitchCharacter(CharactersSO CharactersSO)
    {


        CharacterDisplayMiscEvent.Show(CharactersSO);
        currentCharacterSelected = CharactersSO;
        //if (CharactersSO == null)
        //    return;

        //GameObject gameObject = GetGameObject(currentCharacterSelected);

        //if (gameObject != null)
        //{
        //    gameObject.SetActive(false);
        //}

        //currentCharacterSelected = CharactersSO;
        //gameObject = GetGameObject(currentCharacterSelected);
        //gameObject.SetActive(true);
        //OnCharacterSelected?.Invoke();
    }

    private GameObject GetGameObject(CharactersSO CharactersSO)
    {
        if (CharactersSO == null || !characterObjectsDic.TryGetValue(CharactersSO, out GameObject obj))
            return null;

        return obj;
    }

    private void Start()
    {
    }

    private void InitCharacterObjects()
    {
        if (characterStorage == null)
            return;

        foreach(var characterObject in characterObjectsDic)
        {
            Destroy(characterObject.Value.gameObject);
        }

        characterObjectsDic.Clear();

        foreach (var characterStat in characterStorage.characterStatList)
        {
            CharacterStorage_OnCharacterAdd(characterStat.Key);
        }
    }


    private void OnDestroy()
    {
        CharacterScreenMiscEvent.OnCharacterSceneLoad -= CharacterScreenMiscEvent_OnCharacterSceneLoad;
        if (characterStorage != null)
        {
            characterStorage.OnCharacterAdd -= CharacterStorage_OnCharacterAdd;
        }
    }
}
