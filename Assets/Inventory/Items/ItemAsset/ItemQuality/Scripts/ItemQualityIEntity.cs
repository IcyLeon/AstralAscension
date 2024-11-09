using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemQualityIEntity : ItemQualityButton
{
    [SerializeField] private Image NewImage;
    [field: SerializeField] public ItemQualitySelection ItemQualitySelection { get; private set; }
    public IEntity iEntity { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        OnItemQualityClick += ItemQualityItem_OnItemQualityClick;
    }

    private void UnSubscribeEvents()
    {
        if (iEntity == null)
            return;

        iEntity.OnIEntityChanged -= IEntity_OnIEntityChanged;
    }

    private void SubscribeEvents()
    {
        if (iEntity == null)
            return;

        iEntity.OnIEntityChanged += IEntity_OnIEntityChanged;
    }

    private void IEntity_OnIEntityChanged(IEntityEvents e)
    {
        if (this == null)
            return;

        UpdateVisual();
    }

    public override void SetIItem(IItem iItem)
    {
        base.SetIItem(iItem);
        UnSubscribeEvents();
        iEntity = iItem as IEntity;
        InitIEntity();
        SubscribeEvents();
        UpdateVisual();
    }

    protected virtual void InitIEntity()
    {

    }

    private void ItemQualityItem_OnItemQualityClick(object sender, EventArgs e)
    {
        HideNewStatus();
    }

    public void HideNewStatus()
    {
        if (iEntity == null || !iEntity.IsNew())
            return;

        iEntity.SetNewStatus(false);
    }

    protected virtual void UpdateVisual()
    {
        if (iEntity == null)
            return;

        NewImage.gameObject.SetActive(iEntity.IsNew());
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        UnSubscribeEvents();
    }
}
