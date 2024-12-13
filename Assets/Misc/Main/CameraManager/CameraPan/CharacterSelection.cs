using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public static CharacterSelection instance { get; private set; }
    public CameraPanManager cameraPanManager { get; private set; }

    private CharacterStorage characterStorage;

    private void Awake()
    {
        instance = this;
        cameraPanManager = GetComponentInChildren<CameraPanManager>();
        CharacterManager.OnCharacterStorageNew += DisplayCharacterSelection_OnCharacterStorageNew;
        CharacterManager.OnCharacterStorageOld += DisplayCharacterSelection_OnCharacterStorageOld;
    }

    private void DisplayCharacterSelection_OnCharacterStorageOld(CharacterStorage CharacterStorage)
    {
        CharacterStorage.OnCharacterAdd -= CharacterStorage_OnCharacterAdd;
    }

    private void DisplayCharacterSelection_OnCharacterStorageNew(CharacterStorage CharacterStorage)
    {
        characterStorage = CharacterStorage;

        if (characterStorage != null)
        {
            characterStorage.OnCharacterAdd += CharacterStorage_OnCharacterAdd;
        }
    }

    private void Update()
    {

    }

    private void CharacterStorage_OnCharacterAdd(CharactersSO c)
    {
        Debug.Log(c);
    }

    private void OnDestroy()
    {
        CharacterManager.OnCharacterStorageNew -= DisplayCharacterSelection_OnCharacterStorageNew;
        CharacterManager.OnCharacterStorageOld -= DisplayCharacterSelection_OnCharacterStorageOld;

        if (characterStorage != null)
        {
            characterStorage.OnCharacterAdd -= CharacterStorage_OnCharacterAdd;
        }
    }
}
