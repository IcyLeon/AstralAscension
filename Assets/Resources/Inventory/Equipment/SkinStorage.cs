using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinStorage
{
    public PlayerCharacterProfileSO playerCharacterProfileSO { get; }
    public HashSet<SkinSO> OwnedSkins { get; private set; } = new();
    public SkinSO currentSkinSO { get; private set; }

    public delegate void OnSkinChanged(SkinSO skinSO);
    public event OnSkinChanged OnSkinEquipped, OnSkinOwned;

    public SkinStorage(PlayerCharacterProfileSO PlayerCharacterProfileSO)
    {
        playerCharacterProfileSO = PlayerCharacterProfileSO;

        SkinSO skinSO = playerCharacterProfileSO.DefaultSkinSO;
        OwnSkin(skinSO);
        EquipSkin(skinSO);
    }

    public void EquipSkin(SkinSO SkinSO)
    {
        if (!IsOwned(SkinSO))
        {
            return;
        }

        currentSkinSO = SkinSO;
        OnSkinEquipped?.Invoke(SkinSO);
    }

    public void OwnSkin(SkinSO skinSO)
    {
        if (IsOwned(skinSO))
            return;

        OwnedSkins.Add(skinSO);
        OnSkinOwned?.Invoke(skinSO);
    }

    public bool IsOwned(SkinSO SkinSO)
    {
        return OwnedSkins.Contains(SkinSO);
    }

}
