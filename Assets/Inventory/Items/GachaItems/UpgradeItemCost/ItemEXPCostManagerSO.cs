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
        [field: SerializeField] public int[] EXP { get; private set; }
    }

    [SerializeField] private ItemEXP[] ItemEXPList;

    public int[] GetEXPCostArray(ItemRaritySO itemRaritySO)
    {
        foreach(var itemEXP in ItemEXPList)
        {
            if (itemEXP.ItemRaritySO == itemRaritySO)
                return itemEXP.EXP;
        }

        return null;
    }
}
