using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class UpgradableItems : Item
{
    public bool locked { get; private set; }
    public int maxLevel { get; private set; }
    public UpgradableItems(IItem iItem) : base(iItem)
    {
        locked = false;
        maxLevel = 20;
    }

    public void Upgrade()
    {
        if (amount >= maxLevel)
            return;

        amount++;
        UpgradeItemAction();
        CallOnItemChanged();
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
