using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterTabAttributeActionManager;

public static class TabAttributesMiscEvent
{
    public static event Action<TAB_ATTRIBUTE> OnTabSwitch;
    public static event Action OnTabReset;

    public static void Switch(TAB_ATTRIBUTE TAB_ATTRIBUTE)
    {
        OnTabSwitch?.Invoke(TAB_ATTRIBUTE);
    }

    public static void Reset()
    {
        OnTabReset?.Invoke();
    }
}
