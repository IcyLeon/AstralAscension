using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterTabAttributeActionManager;

public static class TabAttributesMiscEvent
{
    public static event Action<TAB_ATTRIBUTE> OnTabSwitch;

    public static void Switch(TAB_ATTRIBUTE TAB_ATTRIBUTE)
    {
        OnTabSwitch?.Invoke(TAB_ATTRIBUTE);
    }
}
