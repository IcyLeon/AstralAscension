using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaItem : UpgradableItems
{
    public Rarity rarity { get; private set; }

    public GachaItem(Rarity Rarity, IItem iItem) : base(iItem)
    {
        rarity = Rarity;
    }

    public override Rarity GetItemRarity()
    {
        return rarity;
    }
}
