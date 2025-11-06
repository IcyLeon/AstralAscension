using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterScreenPanel : MonoBehaviour
{
    [field: SerializeField] public UpgradeItemContent UpgradeItemContent { get; private set; }
    public CharacterEquipmentManager characterEquipmentManager { get; private set; }
    public event Action OnCharacterIconSelected;

    private void OnEnable()
    {
        OwnedCharacterButtonMiscEvent.OnCharacterIconSelected += OwnedCharacterButtonMiscEvent_OnCharacterIconSelected;
    }

    private void OnDisable()
    {
        OwnedCharacterButtonMiscEvent.OnCharacterIconSelected -= OwnedCharacterButtonMiscEvent_OnCharacterIconSelected;
    }

    private void OwnedCharacterButtonMiscEvent_OnCharacterIconSelected(CharacterEquipmentManager CharacterEquipmentManager)
    {
        characterEquipmentManager = CharacterEquipmentManager;
        OnCharacterIconSelected?.Invoke();
    }
}
