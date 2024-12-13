using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnedCharactersIconManager : MonoBehaviour
{
    [SerializeField] private GameObject CharacterIconPrefab;
    [SerializeField] private Transform ParentTransform;
    private Dictionary<CharactersSO, OwnedCharacterIconButton> characterSOIcons;
    public event Action<CharactersSO> OnIconSelected;
    private CharacterStorage characterStorage;

    private void Awake()
    {
        characterSOIcons = new();
        CharacterManager.OnCharacterStorageOld += CharacterManager_OnCharacterStorageOld;
        CharacterManager.OnCharacterStorageNew += CharacterManager_OnCharacterStorageNew;
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Init()
    {
        if (characterStorage != null)
            return;

        CharacterManager_OnCharacterStorageNew(CharacterManager.instance.characterStorage);
    }

    private void CharacterManager_OnCharacterStorageNew(CharacterStorage CharacterStorage)
    {
        if (characterStorage == CharacterStorage)
            return;

        characterStorage = CharacterStorage;

        if (characterStorage != null)
        {
            characterStorage.OnCharacterAdd += CharacterStorage_OnCharacterAdd;
            characterStorage.OnCharacterRemove += CharacterStorage_OnCharacterRemove;
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
        OwnedCharacterIconButton OwnedCharacterIconButton = Instantiate(CharacterIconPrefab, ParentTransform).GetComponent<OwnedCharacterIconButton>();
        OwnedCharacterIconButton.SetPlayableCharacterSO(CharactersSO);
        OwnedCharacterIconButton.OnCharacterIconSelected += OwnedCharacterIconButton_OnCharacterIconSelected;
        characterSOIcons.Add(CharactersSO, OwnedCharacterIconButton);
    }

    private void OwnedCharacterIconButton_OnCharacterIconSelected(CharactersSO CharactersSO)
    {
        OnIconSelected?.Invoke(CharactersSO);
    }

    private void CharacterManager_OnCharacterStorageOld(CharacterStorage CharacterStorage)
    {
        if (CharacterStorage != null)
        {
            CharacterStorage.OnCharacterAdd -= CharacterStorage_OnCharacterAdd;
            CharacterStorage.OnCharacterRemove -= CharacterStorage_OnCharacterRemove;
        }
    }


    private void OnDestroy()
    {
        CharacterManager.OnCharacterStorageOld -= CharacterManager_OnCharacterStorageOld;
        CharacterManager.OnCharacterStorageNew -= CharacterManager_OnCharacterStorageNew;

        if (characterStorage != null)
        {
            characterStorage.OnCharacterAdd -= CharacterStorage_OnCharacterAdd;
            characterStorage.OnCharacterRemove -= CharacterStorage_OnCharacterRemove;
        }
    }
}
