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

        itemQualityButton = Instantiate(ItemManagerSO.ItemQualityItemPrefab, transform).GetComponent<ItemQualityItem>();
        itemQualityButton.RaycastImage.raycastTarget = false;
        itemQualityButton.SetIEntity(entity);
        SubscribeEvents(entity);
        OnSlotItemAdd?.Invoke(this, new SlotItemEvent
        {
            slot = this,
            iEntity = entity
        });
    }
    private void SubscribeEvents(IEntity iEntity)
    {
        Item item = iEntity as Item;

        if (item == null)
            return;

        item.OnItemChanged += UpgradableItem_OnItemChanged;
    }
    private void UnsubscribeEvents()
    {
        if (itemQualityButton == null)
            return;

        Item item = itemQualityButton.ItemQuality.iItem as Item;

        if (item == null)
            return;

        item.OnItemChanged -= UpgradableItem_OnItemChanged;
    }

    private void UpgradableItem_OnItemChanged(object sender, EventArgs e)
    {
        UpgradableItems upgradableItem = sender as UpgradableItems;
        if (upgradableItem == null)
            return;

        if (upgradableItem.locked || upgradableItem.equipByCharacter != null)
        {
            SetItemQualityButton(null);
        }
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    private void DestroyItemQualityButton()
    {
        if (itemQualityButton == null)
            return;

        UnsubscribeEvents();

        OnSlotItemRemove?.Invoke(this, new SlotItemEvent { 
            slot = this, 
            iEntity = itemQualityButton.ItemQuality.iItem as IEntity
        });

        Destroy(itemQualityButton.gameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        OnSlotClick?.Invoke(this, new SlotEvent { slot = this });
    }
}
