using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterDisplay : MonoBehaviour
{
    private CharacterStorage characterStorage;
    private CharacterSelection characterSelection;
    private Dictionary<CharactersSO, GameObject> characterObjectsDic;
    public CharactersSO currentCharacterSelected { get; private set; }
    public event Action OnCharacterActive;

    // Start is called before the first frame update
    private void Awake()
    {
        characterObjectsDic = new();
        characterSelection = GetComponentInParent<CharacterSelection>();
        characterSelection.OnIconSelected += CharacterSelection_OnIconSelected;

        CharacterManager.OnCharacterStorageNew += DisplayCharacterSelection_OnCharacterStorageNew;
        CharacterManager.OnCharacterStorageOld += DisplayCharacterSelection_OnCharacterStorageOld;
    }

    private void CharacterSelection_OnIconSelected()
    {
        CharactersSO CharactersSO = characterSelection.currentCharacterSelected;

        if (CharactersSO == null)
            return;

        SwitchCharacter(CharactersSO);
    }

    private void CharacterStorage_OnCharacterAdd(CharactersSO c)
    {
        characterObjectsDic.Add(c, null);
    }

    private void SwitchCharacter(CharactersSO CharactersSO)
    {
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

        currentCharacterSelected = CharactersSO;
        OnCharacterActive?.Invoke();
    }

    private GameObject GetGameObject(CharactersSO CharactersSO)
    {
        if (CharactersSO == null || !characterObjectsDic.TryGetValue(CharactersSO, out GameObject obj))
            return null;

        return obj;
    }

    private void Start()
    {
        Init();
        InitSelectedCharacter();
    }

    private void InitSelectedCharacter()
    {
        if (currentCharacterSelected != null)
            return;

        SwitchCharacter(characterObjectsDic.ElementAt(0).Key);
    }

    private void Init()
    {
        if (characterStorage != null)
            return;

        DisplayCharacterSelection_OnCharacterStorageNew(CharacterManager.instance.characterStorage);
    }

    private void DisplayCharacterSelection_OnCharacterStorageOld(CharacterStorage CharacterStorage)
    {
        CharacterStorage.OnCharacterAdd -= CharacterStorage_OnCharacterAdd;
    }

    private void DisplayCharacterSelection_OnCharacterStorageNew(CharacterStorage CharacterStorage)
    {
        if (CharacterStorage == characterStorage)
            return;

        characterStorage = CharacterStorage;

        if (characterStorage != null)
        {
            characterStorage.OnCharacterAdd += CharacterStorage_OnCharacterAdd;
        }
    }

    private void OnDestroy()
    {
        CharacterManager.OnCharacterStorageNew -= DisplayCharacterSelection_OnCharacterStorageNew;
        CharacterManager.OnCharacterStorageOld -= DisplayCharacterSelection_OnCharacterStorageOld;
        DisplayCharacterSelection_OnCharacterStorageOld(characterStorage);
    }
}
