using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterDisplay : MonoBehaviour
{
    private CharacterStorage characterStorage;
    private CharacterSelection characterSelection;
    private Dictionary<CharacterDataStat, GameObject> characterObjectsDic;
    public CharacterDataStat currentCharacterSelected { get; private set; }
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
        CharacterDataStat CharacterDataStat = characterSelection.currentCharacterSelected;

        if (CharacterDataStat == null)
            return;

        SwitchCharacter(CharacterDataStat);
    }

    private void CharacterStorage_OnCharacterAdd(CharactersSO CharactersSO)
    {
        CharacterDataStat c = characterStorage.GetCharacterDataStat(CharactersSO);
        characterObjectsDic.Add(c, null);
    }

    private void SwitchCharacter(CharacterDataStat CharacterDataStat)
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
        currentCharacterSelected = CharacterDataStat;
        OnCharacterActive?.Invoke();
    }

    private GameObject GetGameObject(CharacterDataStat CharacterDataStat)
    {
        if (CharacterDataStat == null || !characterObjectsDic.TryGetValue(CharacterDataStat, out GameObject obj))
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
            InitCharacterObjects();
        }
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
        CharacterManager.OnCharacterStorageNew -= DisplayCharacterSelection_OnCharacterStorageNew;
        CharacterManager.OnCharacterStorageOld -= DisplayCharacterSelection_OnCharacterStorageOld;
        DisplayCharacterSelection_OnCharacterStorageOld(characterStorage);
    }
}
