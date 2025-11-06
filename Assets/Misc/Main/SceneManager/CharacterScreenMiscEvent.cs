using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterScreenMiscEvent
{
    public static event Action<CharacterStorage> OnCharacterSceneLoad;

    public static void Load(CharacterStorage CharacterStorage)
    {
        OnCharacterSceneLoad?.Invoke(CharacterStorage);
    }
}
