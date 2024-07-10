using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class LockItem : MonoBehaviour
{
    protected UpgradableItems upgradableItems { get; private set; }

    [SerializeField] private Image BackgroundImage;
    [SerializeField] private Image LockImage;
    [SerializeField] private LockItemInfoSO LockItemInfoSO;

    public void SetUpgradableItem(Item Item)
    {
        UnsubscribeEvents();
        upgradableItems = Item as UpgradableItems;
        SubscribeEvents();

        gameObject.SetActive(upgradableItems != null);
    }
    private void SubscribeEvents()
    {
        if (upgradableItems == null)
            return;

        upgradableItems.OnItemChanged += UpgradableItems_OnItemChanged;
        UpdateVisual();
    }

    private void UnsubscribeEvents()
    {
        if (upgradableItems == null)
            return;

        upgradableItems.OnItemChanged -= UpgradableItems_OnItemChanged;
    }

    private void UpgradableItems_OnItemChanged(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    protected void UpdateVisual()
    {
        if (upgradableItems == null)
            return;

        RefreshLock();
    }


    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    protected virtual void RefreshLock()
    {
        LockItemInfoSO.LockInfo lockInfo = LockItemInfoSO.GetLockInfo(upgradableItems.locked);

        LockImage.sprite = lockInfo.LockImage;
        LockImage.color = lockInfo.LockImageColor;
        BackgroundImage.color = lockInfo.LockBackgroundColor;
    }
}
