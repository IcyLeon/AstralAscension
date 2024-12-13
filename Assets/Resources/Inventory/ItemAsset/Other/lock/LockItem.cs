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

    public void SetIItem(IItem iItem)
    {
        UnsubscribeEvents();

        upgradableItems = iItem as UpgradableItems;

        gameObject.SetActive(upgradableItems != null);

        SubscribeEvents();
    }

    private void UnsubscribeEvents()
    {
        if (upgradableItems == null)
            return;

        upgradableItems.OnIEntityChanged -= UpgradableItems_OnIEntityChanged;
    }

    private void SubscribeEvents()
    {
        if (upgradableItems == null)
            return;

        upgradableItems.OnIEntityChanged += UpgradableItems_OnIEntityChanged;
        UpdateVisual();
    }

    private void UpgradableItems_OnIEntityChanged(IEntity e)
    {
        if (this == null)
            return;

        UpdateVisual();
    }

    protected virtual void UpdateVisual()
    {
        if (upgradableItems == null)
            return;

        RefreshLock();
    }

    private void RefreshLock()
    {
        LockItemInfoSO.LockInfo lockInfo = LockItemInfoSO.GetLockInfo(upgradableItems.locked);

        LockImage.sprite = lockInfo.LockImage;
        LockImage.color = lockInfo.LockImageColor;
        BackgroundImage.color = lockInfo.LockBackgroundColor;
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }
}
