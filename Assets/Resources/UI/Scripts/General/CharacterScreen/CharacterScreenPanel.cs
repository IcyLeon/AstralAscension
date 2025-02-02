using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterScreenPanel : MonoBehaviour
{
    private OwnedCharactersIconManager OwnedCharactersIconManager;
    public CharacterDataStat currentCharacterSelected { get; private set; }
    public event Action OnIconSelected;

    private void Awake()
    {
        OwnedCharactersIconManager = GetComponentInChildren<OwnedCharactersIconManager>();
        OwnedCharactersIconManager.OnIconSelected += OwnedCharactersIconManager_OnIconSelected;
    }

    private void InitCharacterDisplayManager()
    {
        CharacterDisplayManager CharacterDisplayManager = CharacterDisplayManager.instance;
        if (CharacterDisplayManager == null)
        {
            return;
        }
        CharacterDisplayManager.SetScreenPanel(this);
    }

    private void Start()
    {
        InitCharacterDisplayManager();
    }

    private void UpdateVisual()
    {
        currentCharacterSelected = OwnedCharactersIconManager.selectedIcon.characterDataStat;
        OnIconSelected?.Invoke();
    }

    private void OwnedCharactersIconManager_OnIconSelected()
    {
        UpdateVisual();
    }

    private void OnDestroy()
    {
        OwnedCharactersIconManager.OnIconSelected -= OwnedCharactersIconManager_OnIconSelected;
    }
}
