using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public event Action<Slot> OnSlotClick;
    public event Action<Slot> OnSlotItemAdd;
    public event Action<IData, Slot> OnSlotItemRemove;

    public ItemQualityIEntity itemQuality
    {
        get
        {
            return GetComponentInChildren<ItemQualityIEntity>();
        }
    }


    public void OnDrop(PointerEventData eventData)
    {
    }

    public void Init()
    {
        SubscribeEvents();
        OnSlotItemAdd?.Invoke(this);
    }


    public void DeleteIItem()
    {
        if (itemQuality == null)
            return;

        IEntity entity = itemQuality.iEntity;
        UnsubscribeEvents();
        Destroy(itemQuality.gameObject);
        OnSlotItemRemove?.Invoke(entity, this);
    }

    private void SubscribeEvents()
    {
        if (itemQuality == null)
            return;

        itemQuality.iEntity.OnIEntityChanged += IEntity_OnIEntityChanged;
    }

    private void IEntity_OnIEntityChanged(IEntity IEntity)
    {
        UpgradableItems upgradableItem = IEntity as UpgradableItems;

        if (upgradableItem == null || (!upgradableItem.locked || upgradableItem.equipByCharacter != null))
            return;

        DeleteIItem();
    }

    private void UnsubscribeEvents()
    {
        if (itemQuality == null)
            return;

        itemQuality.iEntity.OnIEntityChanged -= IEntity_OnIEntityChanged;
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        OnSlotClick?.Invoke(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1.05f, 0.075f).SetEase(Ease.InOutSine);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1f, 0.075f).SetEase(Ease.InOutSine);
    }
}
