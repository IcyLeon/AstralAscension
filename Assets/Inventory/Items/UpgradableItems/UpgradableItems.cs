using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradableItems : Item
{
    public int maxLevel { get; private set; }

    public UpgradableItems(Rarity Rarity, IItem iItem) : base(Rarity, iItem)
    {
        amount = 0;
        maxLevel = 20;
    }

    public void UpgradeItem()
    {
        if (amount >= maxLevel)
            return;

        amount++;

        CallOnItemChanged();
    }
}
