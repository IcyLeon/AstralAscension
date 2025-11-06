using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterManagerMiscEvent
{
    public static Action<CharacterStorage> OnCharacterStorageSend;

    public static void SendCharacterStorage(CharacterStorage characterStorage)
    {
        OnCharacterStorageSend?.Invoke(characterStorage);
    }
}
