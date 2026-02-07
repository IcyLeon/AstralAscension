using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ItemQuality))]
public abstract class ItemQualityIEntity : MonoBehaviour
{
    [SerializeField] private Image NewImage;
    [field: SerializeField] public ItemQuality itemQuality { get; private set; }
    [field: SerializeField] public ItemQualitySelection ItemQualitySelection { get; private set; }
    public IEntity iEntity { get; private set; }
    public event Action OnIEntityChanged;

    protected virtual void Awake()
    {
        itemQuality.OnItemQualitySelect += ItemQualityItem_OnItemQualitySelect;
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


    public virtual void SetIData(ItemQualityIEntityDisplayData ItemQualityIEntityDisplayData)
    {
        itemQuality.SetIData(ItemQualityIEntityDisplayData);
        UnSubscribeEvents();
        iEntity = ItemQualityIEntityDisplayData.iEntity;
        SubscribeEvents();
    }

    private void ItemQualityItem_OnItemQualitySelect(IData IData)
    {
        if (iEntity == null || !iEntity.IsNew())
            return;

        iEntity.SetNewStatus(false);
        itemQuality.OnItemQualitySelect -= ItemQualityItem_OnItemQualitySelect;
    }

    protected virtual void UpdateVisual()
    {
        if (iEntity == null)
            return;

        NewImage.gameObject.SetActive(iEntity.IsNew());
        itemQuality.UpdateDisplayText();
    }

    protected virtual void OnDestroy()
    {
        UnSubscribeEvents();
        itemQuality.OnItemQualitySelect -= ItemQualityItem_OnItemQualitySelect;
    }

}
