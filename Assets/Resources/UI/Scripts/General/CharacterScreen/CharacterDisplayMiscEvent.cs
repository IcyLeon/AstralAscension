using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterDisplayMiscEvent
{
    public static event Action<CharactersSO, ModelsHandler> OnCharacterShowcase;

    public static void Show(CharactersSO CharactersSO, ModelsHandler ModelsHandler)
    {
        OnCharacterShowcase?.Invoke(CharactersSO, ModelsHandler);
    }
}
