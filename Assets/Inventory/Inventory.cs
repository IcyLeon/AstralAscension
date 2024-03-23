using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public int mora { get; private set; }
    //public Dictionary<Item>

    public void AddMora(int Amt)
    {
        mora += Amt;
        mora = Mathf.Clamp(mora, 0, 100000000);
    }

    public Inventory()
    {
        mora = 0; 
    }
}
