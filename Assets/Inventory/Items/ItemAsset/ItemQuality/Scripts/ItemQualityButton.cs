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
    public IItem iItem { get; private set; }

    protected virtual void Awake()
    {
        RaycastImage = GetComponent<Image>();
    }

    public virtual void SetIItem(IItem IItem)
    {
        iItem = IItem;
        ItemQuality.SetIItem(IItem);
    }

    protected virtual void OnDestroy()
    {
        OnItemQualityClick = null;
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
