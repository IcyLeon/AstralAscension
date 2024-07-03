using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItem : Item
{
    public void AddAmount(int amount)
    {
        this.amount += amount;
        CallOnItemChanged();
    }

    public ConsumableItem(Rarity Rarity, IItem iItem) : base(Rarity, iItem)
    {
        amount = 1;
    }
}
