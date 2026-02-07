using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterScreenMiscEvent
{
    public static event Action<CharacterStorage> OnSceneLoad;
    public static event Action OnSceneUnload;

    public static void Load(CharacterStorage CharacterStorage)
    {
        OnSceneLoad?.Invoke(CharacterStorage);
    }

    public static void Unload()
    {
        OnSceneUnload?.Invoke();
    }
}
