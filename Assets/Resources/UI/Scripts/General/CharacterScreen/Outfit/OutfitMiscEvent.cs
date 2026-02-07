using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OutfitMiscEvent
{
    public delegate void OnSkinChanged(SkinSO skin);
    public static event OnSkinChanged OnSkinSelected, OnSkinApply;

    public static void Select(SkinSO SkinSO)
    {
        OnSkinSelected?.Invoke(SkinSO);
    }

    public static void ApplySkin(SkinSO SkinSO)
    {
        OnSkinApply?.Invoke(SkinSO);
    }
}
