using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[DisallowMultipleComponent]
public abstract class ItemQualityButton : MonoBehaviour, IPointerClickHandler
{
    [field: SerializeField] public ItemQuality ItemQuality { get; private set; }
    public event EventHandler OnItemQualityClick;

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
        OnItemQualityClick?.Invoke(this, EventArgs.Empty);
    }
}
