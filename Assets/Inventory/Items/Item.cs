using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public Rarity rarity { get; private set; }
    public ItemSO itemSO { get; private set; }

    public Item(Rarity Rarity, ItemSO ItemSO)
    {
        rarity = Rarity;
        itemSO = ItemSO;
    }
}
