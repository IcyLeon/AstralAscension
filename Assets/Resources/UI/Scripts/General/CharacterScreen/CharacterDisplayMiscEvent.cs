using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterDisplayMiscEvent
{
    public static event Action<CharactersSO> OnCharacterShowcase;

    public static void Show(CharactersSO CharactersSO)
    {
        OnCharacterShowcase?.Invoke(CharactersSO);
    }
}
