using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemEXPCostManagerSO", menuName = "ScriptableObjects/ItemManager/ItemEXPCostManagerSO")]
public class ItemEXPCostManagerSO : ScriptableObject
{
    [Serializable]
    public class ItemEXP
    {
        [field: SerializeField] public ItemRaritySO ItemRaritySO { get; private set; }
        [field: SerializeField] public int BaseEnhancementEXP { get; private set; }
        [field: SerializeField] public int[] EXP { get; private set; }
    }

    [SerializeField] private ItemEXP[] ItemEXPList;

    private ItemEXP GetItemEXP(Rarity rarity)
    {
        foreach(var itemEXP in ItemEXPList)
        {
            if (itemEXP.ItemRaritySO.Rarity == rarity)
                return itemEXP;
        }

        return null;
    }

    public int GetMaxLevel(Rarity rarity)
    {
        ItemEXP itemEXP = GetItemEXP(rarity);

        if (itemEXP == null)
        {
            return 1;
        }

        return itemEXP.EXP.Length;
    }

    public int GetRequiredEXP(int currentlevel, Rarity rarity)
    {
        ItemEXP itemEXP = GetItemEXP(rarity);

        if (currentlevel >= itemEXP.EXP.Length)
            return 0;

        return itemEXP.EXP[currentlevel];
    }

    public int GetTotalExpRequired(int currentlevel, Rarity rarity)
    {
        int totalExp = 0;

        ItemEXP itemEXP = GetItemEXP(rarity);

        if (itemEXP == null)
        {
            return totalExp;
        }

        for (int i = 0; i < currentlevel; i++)
        {
            totalExp += itemEXP.EXP[i];
        }

        return totalExp;
    }

    public int GetBaseEnhancementEXP(Rarity rarity)
    {
        ItemEXP itemEXP = GetItemEXP(rarity);

        if (itemEXP == null)
        {
            return 0;
        }

        return itemEXP.BaseEnhancementEXP;
    }
}
