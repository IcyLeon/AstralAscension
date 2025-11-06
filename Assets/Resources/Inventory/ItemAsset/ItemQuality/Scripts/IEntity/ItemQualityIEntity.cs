using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemQualityIEntity : ItemQuality
{
    [SerializeField] private Image NewImage;
    [field: SerializeField] public ItemQualitySelection ItemQualitySelection { get; private set; }
    public IEntity iEntity { get; private set; }
    public event Action OnIEntityChanged;

    protected override void Awake()
    {
        base.Awake();
        OnItemQualitySelect += ItemQualityItem_OnItemQualitySelect;
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
        UpdateVisual();
    }

    private void IEntity_OnIEntityChanged(IEntity entity)
    {
        if (this == null)
            return;

        UpdateVisual();
        OnIEntityChanged?.Invoke();
    }

    public override void SetIItem(IData iData)
    {
        base.SetIItem(iData);
        UnSubscribeEvents();
        iEntity = iData as IEntity;
        InitIEntity();
        SubscribeEvents();
    }

    protected virtual void InitIEntity()
    {

    }

    private void ItemQualityItem_OnItemQualitySelect(ItemQuality ItemQuality)
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

    public override void OnSelect()
    {
        base.OnSelect();
    }

    public override void OnDeSelect()
    {
        base.OnDeSelect();
        HideNewStatus();
    }
}
