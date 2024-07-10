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

    public void Use(int amount)
    {
        amount = -Mathf.Abs(amount);
        AddAmount(amount);
    }

    public ConsumableItem(IItem iItem) : base(iItem)
    {
        amount = 1;
    }
}
