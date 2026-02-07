using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountCharacters : MonoBehaviour
{
    public static AccountCharacters instance { get; private set; }
    public CharacterStorage mainCharacterStorage { get; private set; }
    public static event Action<CharacterStorage> OnCharacterStorageChanged;
    
    private void Awake()
    {
        instance = this;
    }

    private void SetCharacterStorage(CharacterStorage CharacterStorage)
    {
        mainCharacterStorage = CharacterStorage;
        OnCharacterStorageChanged?.Invoke(mainCharacterStorage);
    }

    private void Start()
    {
        SetCharacterStorage(new CharacterStorage());
        LoadCharacters();
        PartySystem.instance.TestAddMembers();
    }

    private void LoadCharacters()
    {
        //foreach (var CharactersSO in CharacterManager.instance.playableCharactersSOs.Keys)
        //{
        //    mainCharacterStorage.AddCharacterData(CharactersSO, CharactersSO.CreateCharacterDataStat());
        //}
    }

    private void Update()
    {
        UpdateCharactersData();
    }

    private void UpdateCharactersData()
    {
        if (mainCharacterStorage == null)
            return;

        mainCharacterStorage.Update();
    }
}
