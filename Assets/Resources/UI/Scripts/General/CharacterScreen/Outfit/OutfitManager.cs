using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class OutfitManager : MonoBehaviour
{
    public PlayerCharacterProfileSO playerCharacterProfileSO { get; private set; }
    private Dictionary<SkinSO, GameObject> skinList = new();
    private CharacterScreenPanel characterScreenPanel;

    private void Awake()
    {
        characterScreenPanel = GetComponentInParent<CharacterScreenPanel>();
        characterScreenPanel.OnCharacterIconSelected += CharacterScreenPanel_OnCharacterIconSelected;
    }

    private void CharacterScreenPanel_OnCharacterIconSelected()
    {
        playerCharacterProfileSO = characterScreenPanel.characterEquipmentManager.charactersSO.ProfileSO as PlayerCharacterProfileSO;
    }

    private void OnDestroy()
    {
        characterScreenPanel.OnCharacterIconSelected -= CharacterScreenPanel_OnCharacterIconSelected;
    }
}
