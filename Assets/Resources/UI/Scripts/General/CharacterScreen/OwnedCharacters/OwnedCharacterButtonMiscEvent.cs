using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OwnedCharacterButtonMiscEvent
{
    public static event Action<CharacterEquipmentManager> OnCharacterIconSelected;

    public static void Select(CharacterEquipmentManager CharacterEquipmentManager)
    {
        OnCharacterIconSelected?.Invoke(CharacterEquipmentManager);
    }
}
