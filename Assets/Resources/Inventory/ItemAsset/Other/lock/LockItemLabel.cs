using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockItemLabel : LockItem
{
    protected override void UpdateVisual()
    {
        base.UpdateVisual();

        if (upgradableItems == null)
            return;

        this.gameObject.SetActive(upgradableItems.locked);
    }
}
