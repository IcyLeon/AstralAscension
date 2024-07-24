using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemQualityEvents : EventArgs
{
    public ItemQualityButton ItemQualityButton;
}

[DisallowMultipleComponent]
public abstract class ItemQualityButton : MonoBehaviour, IPointerClickHandler
{
    public Image RaycastImage { get; private set; }
    [field: SerializeField] public ItemQuality ItemQuality { get; private set; }
    public event EventHandler<ItemQualityEvents> OnItemQualityClick;

    protected virtual void Awake()
    {
        RaycastImage = GetComponent<Image>();
    }

    protected void SetInterfaceItem(IItem IItem)
    {
        if (ItemQuality == null)
            return;

        ItemQuality.SetInterfaceItem(IItem);
    }

    protected void UpdateDisplayText(string txt)
    {
        if (ItemQuality == null)
            return;

        ItemQuality.UpdateDisplayText(txt);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        OnItemQualityClick?.Invoke(this, new ItemQualityEvents
        {
            ItemQualityButton = this
        });
    }
}
