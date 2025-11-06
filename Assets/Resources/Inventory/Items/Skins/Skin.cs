using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : IData
{
    private SkinSO SkinSO;
    public Skin(SkinSO skinSO)
    {
        SkinSO = skinSO;
    }

    public string GetName()
    {
        return SkinSO.GetName();
    }

    public Sprite GetIcon()
    {
        return SkinSO.GetIcon();
    }

    public string GetDescription()
    {
        return SkinSO.GetDescription();
    }

    public ItemTypeSO GetTypeSO()
    {
        return SkinSO.GetTypeSO();
    }

    public ItemRaritySO GetRaritySO()
    {
        return SkinSO.GetRaritySO();
    }
}
