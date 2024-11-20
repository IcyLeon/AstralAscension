using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[DisallowMultipleComponent]
public abstract class ItemQualityButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image RaycastImage { get; private set; }
    [field: SerializeField] public ItemQuality ItemQuality { get; private set; }
    public event Action<ItemQualityButton> OnItemQualityClick;
    public IItem iItem { get; private set; }

    protected virtual void Awake()
    {
        RaycastImage = ItemQuality.GetComponent<Image>();
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

        Select();
    }

    public void Select()
    {
        OnItemQualityClick?.Invoke(this);
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
