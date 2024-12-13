using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItem : Item
{
    public override bool IsStackable()
    {
        return true;
    }

    public void Use(int amount)
    {
        amount = -Mathf.Abs(amount);
        AddAmount(amount);
    }

    public ConsumableItem(IItem iItem) : base(iItem)
    {
    }
}
