using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockItemButton : LockItem
{
    private Button lockButton;

    private void Awake()
    {
        lockButton = GetComponent<Button>();

        if (lockButton == null)
            return;

        lockButton.onClick.AddListener(ToggleLock);
    }
    private void ToggleLock()
    {
        if (upgradableItems == null)
            return;

        upgradableItems.SetLockStatus(!upgradableItems.locked);
        UpdateVisual();
    }

}
