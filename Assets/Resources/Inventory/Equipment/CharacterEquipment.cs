using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class CharacterEquipment
{
    public CharacterEquipment()
    {

    }

    public abstract void Equip(IData Item);

    public abstract void UnEquip(IData Item);

    public abstract IData GetExistingItem(IData Item);
}
