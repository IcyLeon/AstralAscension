using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradableItems : Item
{
    public CharactersSO equipByCharacter { get; private set; }

    public bool locked { get; private set; }
    public int maxLevel { get; private set; }
    public UpgradableItems(IItem iItem) : base(iItem)
    {
        locked = false;
        maxLevel = 20;
    }

    public virtual void SetEquip(CharactersSO charactersSO)
    {
        equipByCharacter = charactersSO;
        CallOnItemChanged();
    }

    public void Upgrade()
    {
        if (amount >= maxLevel)
            return;

        amount++;
        UpgradeItemAction();
        CallOnItemChanged();
    }

    public override void AddAmount(int amount)
    {
    }

    public override bool IsStackable()
    {
        return false;
    }

    public void SetLockStatus(bool locked)
    {
        this.locked = locked;
        CallOnItemChanged();
    }

    protected virtual void UpgradeItemAction()
    {

    }
}