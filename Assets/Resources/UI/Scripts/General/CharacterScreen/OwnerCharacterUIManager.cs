using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OwnerCharacterUIManager : MonoBehaviour
{
    private OwnedCharactersIconManager OwnedCharactersIconManager;
    public CharactersSO currentCharacterSelected { get; private set; }
    public event Action<CharactersSO> OnIconSelected;

    private void Awake()
    {
        OwnedCharactersIconManager = GetComponentInChildren<OwnedCharactersIconManager>(true);
        OwnedCharactersIconManager.OnIconSelected += OwnedCharactersIconManager_OnIconSelected;
    }

    private void OwnedCharactersIconManager_OnIconSelected(CharactersSO CharactersSO)
    {
        currentCharacterSelected = CharactersSO;
        OnIconSelected?.Invoke(currentCharacterSelected);
    }

    private void OnDestroy()
    {
        OwnedCharactersIconManager.OnIconSelected -= OwnedCharactersIconManager_OnIconSelected;
    }
}
