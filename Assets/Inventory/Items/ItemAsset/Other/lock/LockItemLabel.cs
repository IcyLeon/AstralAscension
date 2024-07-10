using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockItemLabel : LockItem
{
    protected override void RefreshLock()
    {
        base.RefreshLock();

        gameObject.SetActive(upgradableItems.locked);
    }
}
