using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotItemEvent : SlotEvent
{
    public IEntity iEntity;
}

public class Slot : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    [SerializeField] private ItemManagerSO ItemManagerSO;
    public event EventHandler<SlotEvent> OnSlotClick;
    public event EventHandler<SlotItemEvent> OnSlotItemAdd;
    public event EventHandler<SlotItemEvent> OnSlotItemRemove;

    public ItemQualityIEntity itemQualityButton { get; private set; }

    public void OnDrop(PointerEventData eventData)
    {
    }

    private void Awake()
    {
        OnSlotItemAdd += Slot_OnSlotItemAdd;
    }

    private void Slot_OnSlotItemAdd(object sender, SlotItemEvent e)
    {
        HideNewStatus(itemQualityButton);
    }

    private void HideNewStatus(ItemQualityIEntity ItemQualityIEntity)
    {
        ItemQualityItem itemQualityItem = ItemQualityIEntity as ItemQualityItem;

        if (itemQualityItem == null)
            return;

        itemQualityItem.HideNewStatus();
    }

    public void SetItemQualityButton(IEntity entity)
    {
        if (entity == null || itemQualityButton != null)
        {
            DestroyItemQualityButton();

            if (entity == null)
            {
                return;
            }
        }

        itemQualityButton = ItemManagerSO.CreateItemQualityItem(entity, transform);
        itemQualityButton.RaycastImage.raycastTarget = false;
        SubscribeEvents();
        OnSlotItemAdd?.Invoke(this, new SlotItemEvent
        {
            slot = this,
            iEntity = entity
        });
    }
    private void SubscribeEvents()
    {
        if (itemQualityButton == null || itemQualityButton.iEntity == null)
            return;

        itemQualityButton.iEntity.OnIEntityChanged += IEntity_OnIEntityChanged;
    }

    private void IEntity_OnIEntityChanged(IEntityEvents e)
    {
        UpgradableItems upgradableItem = e.iEntity as UpgradableItems;

        if (upgradableItem == null)
            return;

        if (upgradableItem.locked || upgradableItem.equipByCharacter != null)
        {
            SetItemQualityButton(null);
        }
    }

    private void UnsubscribeEvents()
    {
        if (itemQualityButton == null || itemQualityButton.iEntity == null)
            return;

        itemQualityButton.iEntity.OnIEntityChanged -= IEntity_OnIEntityChanged;
    }

    private void OnDestroy()
    {
        OnSlotItemAdd -= Slot_OnSlotItemAdd;
        UnsubscribeEvents();
    }


    private void DestroyItemQualityButton()
    {
        if (itemQualityButton == null)
            return;

        UnsubscribeEvents();

        IEntity iEntity = itemQualityButton.iItem as IEntity;

        Destroy(itemQualityButton.gameObject);
        itemQualityButton = null;

        OnSlotItemRemove?.Invoke(this, new SlotItemEvent
        {
            slot = this,
            iEntity = iEntity
        });
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        OnSlotClick?.Invoke(this, new SlotEvent { slot = this });
    }
}
