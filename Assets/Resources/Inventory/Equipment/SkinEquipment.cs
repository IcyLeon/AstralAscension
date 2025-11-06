using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinEquipment
{
    private PlayerCharactersSO playerCharactersSO;
    public Dictionary<SkinSO, SkinSO> OwnedSkins { get; private set; } = new();
    public SkinSO currentSkinSO;

    public delegate void OnSkinChanged(SkinSO skinSO); 
    public event OnSkinChanged OnSkinOwned;
    public event OnSkinChanged OnSkinSwitch;


    public SkinEquipment(PlayerCharactersSO PlayerCharactersSO)
    {
        playerCharactersSO = PlayerCharactersSO;
    }

    public void EquipSkin(SkinSO SkinSO)
    {
        currentSkinSO = SkinSO;
        OnSkinSwitch?.Invoke(SkinSO);
    }

    public void UnEquipSkin(SkinSO SkinSO)
    {
        currentSkinSO = null;
    }

    public PlayerCharacterProfileSO GetPlayerCharacterProfileSO()
    {
        return playerCharactersSO.GetPlayerCharacterProfileSO();
    }

    public void OwnSkin(SkinSO skinSO)
    {
        if (IsOwned(skinSO))
            return;

        OwnedSkins.Add(skinSO, skinSO);
        OnSkinOwned?.Invoke(skinSO);
    }

    public bool IsOwned(SkinSO SkinSO)
    {
        return OwnedSkins.ContainsKey(SkinSO);
    }

}
