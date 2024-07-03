using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemQualityItem : ItemQualityButton
{
    private Item item;

    private void OnDestroyItem()
    {
        if (item == null)
            return;

        item.OnItemChanged -= Item_OnItemChanged;
    }

    public void SetItem(Item Item)
    {
        OnDestroyItem();

        item = Item;

        if (item == null)
            return;

        item.OnItemChanged += Item_OnItemChanged;

        SetInterfaceItem(item);

        UpdateVisuals();
    }

    private void OnDestroy()
    {
        OnDestroyItem();
    }

    private void UpdateVisuals()
    {
        string displayText = "";

        if (item is UpgradableItems)
        {
            displayText = "+";
        }

        displayText += item.amount;

        UpdateDisplayText(displayText);
    }

    private void Item_OnItemChanged(object sender, EventArgs e)
    {
        UpdateVisuals();
    }
}
