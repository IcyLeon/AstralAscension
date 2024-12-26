using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    private CharacterScreenPanel characterScreenPanel;
    private CharacterRT_DragRotation characterRT;
    public CharactersSO currentCharacterSelected { get; private set; }
    public CameraPanManager cameraPanManager { get; private set; }
    private CharacterDisplay characterDisplay;
    public event Action OnIconSelected;
    public event Action OnCharacterSelected;

    private void Awake()
    {
        cameraPanManager = GetComponentInChildren<CameraPanManager>();
        characterDisplay = GetComponentInChildren<CharacterDisplay>();
        characterDisplay.OnCharacterActive += CharacterDisplay_OnCharacterSelected;
    }

    private void CharacterDisplay_OnCharacterSelected()
    {
        SetSelectedCharacter(characterDisplay.currentCharacterSelected);
        OnCharacterSelected?.Invoke();
    }

    private void SetSelectedCharacter(CharactersSO CharactersSO)
    {
        currentCharacterSelected = CharactersSO;
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

        characterRT = characterScreenPanel.GetComponentInChildren<CharacterRT_DragRotation>();
        characterScreenPanel.OnIconSelected += CharacterScreenPanel_OnIconSelected;
        SubscribeCharacterRTEvents();
        UpdateVisual();
    }

    private void SubscribeCharacterRTEvents()
    {
        if (characterRT == null)
            return;

        characterRT.OnMove += CharacterRT_OnMove;
        characterRT.OnZoom += CharacterRT_OnZoom;
    }

    private void UnsubscribeCharacterRTEvents()
    {
        if (characterRT == null)
            return;

        characterRT.OnMove -= CharacterRT_OnMove;
        characterRT.OnZoom -= CharacterRT_OnZoom;
    }

    private void CharacterRT_OnZoom(float delta)
    {
        cameraPanManager.OnScroll(delta);
    }

    private void CharacterRT_OnMove(Vector2 delta)
    {
        cameraPanManager.OnDrag(delta);
    }

    private void UnsubscribeEvents()
    {
        UnsubscribeCharacterRTEvents();

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
        SetSelectedCharacter(characterScreenPanel.currentCharacterSelected);
        OnIconSelected?.Invoke();
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
        characterDisplay.OnCharacterActive -= CharacterDisplay_OnCharacterSelected;
    }
}
