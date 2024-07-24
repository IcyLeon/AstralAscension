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

    private void OnDestroy()
    {
        OnDestroyItem();
        OnItemQualityClick -= ItemQualityItem_OnItemQualityClick;
    }

    protected override void UpdateVisual()
    {
        string displayText = "";

        if (item is UpgradableItems)
        {
            displayText = "+";
        }

        displayText += item.amount;

        UpdateLockVisual();

        UpdateDisplayText(displayText);

        NewImage.gameObject.SetActive(item.newStatus);
    }

    private void Item_OnItemChanged(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateLockVisual()
    {
        LockItem.SetUpgradableItem(item);
        EquipIcon.UpdateVisual(item);
    }
}
