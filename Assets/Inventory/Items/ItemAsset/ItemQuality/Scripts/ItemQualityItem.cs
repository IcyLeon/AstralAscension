using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemQualityItem : ItemQualityIEntity
{
    [SerializeField] private Image NewImage;
    [SerializeField] private LockItem LockItem;
    [SerializeField] private EquipIcon EquipIcon;
    private Item item
    {
        get
        {
            return iEntity as Item;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        OnItemQualityClick += ItemQualityItem_OnItemQualityClick;
    }

    private void ItemQualityItem_OnItemQualityClick(object sender, EventArgs e)
    {
        if (item == null || !item.newStatus)
            return;

        item.SetNewStatus(false);
    }

    private void OnDestroyItem()
    {
        if (item == null)
            return;

        item.OnItemChanged -= Item_OnItemChanged;
    }

    public override void SetIEntity(IEntity entity)
    {
        OnDestroyItem();

        base.SetIEntity(entity);

        if (item == null)
            return;

        item.OnItemChanged += Item_OnItemChanged;

        UpdateVisual();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        OnDestroyItem();
    }

    protected override void UpdateVisual()
    {
        if (item == null)
            return;

        string displayText = "";

        if (item is UpgradableItems)
        {
            displayText = "+";
        }

        displayText += item.amount;

        UpdateDisplayText(displayText);

        NewImage.gameObject.SetActive(item.newStatus);

        UpdateLockVisual();
    }

    private void Item_OnItemChanged(object sender, EventArgs e)
    {
        if (this == null)
            return;

        UpdateVisual();
    }

    private void UpdateLockVisual()
    {
        EquipIcon.UpdateVisual(item);
        LockItem.SetUpgradableItem(item);
    }
}
