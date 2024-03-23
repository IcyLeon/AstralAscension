using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{
    public int mora { get; private set; }
    private Dictionary<Item, ItemSO> ItemList;
    //private Dictionary<PlayerCharactersSO, C>
    public void AddMora(int Amt)
    {
        mora += Amt;
        mora = Mathf.Clamp(mora, 0, 100000000);
    }

    public void AddItem(Item item)
    {
    }

    public void RemoveItem(Item item)
    {

    }

    private Item isItemExisted(ItemSO itemSO)
    {
        foreach(var item in ItemList)
        {
            if (item.Key.itemSO == itemSO)
                return item.Key;
        }

        return null;
    }

    public Inventory(int StartingMora = 0)
    {
        mora = StartingMora;
        ItemList = new();
    }
}
