using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockItemButton : LockItem
{
    public void ToggleLock()
    {
        if (upgradableItems == null)
            return;

        upgradableItems.SetLockStatus(!upgradableItems.locked);
        UpdateVisual();
    }

}
