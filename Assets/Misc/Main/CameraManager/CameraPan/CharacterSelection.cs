using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    private CharacterScreenPanel characterScreenPanel;
    public CharacterDataStat currentCharacterSelected { get; private set; }
    private CharacterDisplay characterDisplay;
    public event Action OnIconSelected;
    public event Action OnCharacterSelected;

    private void Awake()
    {
        characterDisplay = GetComponentInChildren<CharacterDisplay>();
        characterDisplay.OnCharacterActive += CharacterDisplay_OnCharacterSelected;
    }

    private void CharacterDisplay_OnCharacterSelected()
    {
        SetSelectedCharacter(characterDisplay.currentCharacterSelected);
        OnCharacterSelected?.Invoke();
    }

    private void SetSelectedCharacter(CharacterDataStat CharacterDataStat)
    {
        currentCharacterSelected = CharacterDataStat;
    }

    public void SetScreenPanel(CharacterScreenPanel Panel)
    {
        UnsubscribeEvents();
        characterScreenPanel = Panel;
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        if (characterScreenPanel == null)
            return;

        characterScreenPanel.OnIconSelected += CharacterScreenPanel_OnIconSelected;
        UpdateVisual();
    }

    private void UnsubscribeEvents()
    {
        if (characterScreenPanel == null)
            return;

        characterScreenPanel.OnIconSelected -= CharacterScreenPanel_OnIconSelected;
    }

    private void CharacterScreenPanel_OnIconSelected()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        CharacterDataStat characterDataStat = characterScreenPanel.currentCharacterSelected;

        if (characterDataStat == null)
            return;

        SetSelectedCharacter(characterDataStat);
        OnIconSelected?.Invoke();
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
        characterDisplay.OnCharacterActive -= CharacterDisplay_OnCharacterSelected;
    }
}
