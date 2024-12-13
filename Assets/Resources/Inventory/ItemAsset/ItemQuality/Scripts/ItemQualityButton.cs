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
    public event Action<ItemQualityButton> OnItemQualitySelect;
    public event Action<ItemQualityButton> OnItemQualityDestroyed;

    protected virtual void Awake()
    {
        RaycastImage = ItemQuality.GetComponent<Image>();
    }

    public virtual void SetIItem(IItem IItem)
    {
        ItemQuality.SetIItem(IItem);
    }

    protected virtual void OnDestroy()
    {
        OnItemQualitySelect = null;
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
        OnItemQualitySelect?.Invoke(this);
    }

    public void Destroy()
    {
        OnItemQualityDestroyed?.Invoke(this);
        Destroy(gameObject);
    }

    public virtual void OnSelect()
    {
        transform.DOScale(1.1f, 0.075f).SetEase(Ease.InOutSine);
    }

    public virtual void OnDeSelect()
    {
        transform.DOScale(1f, 0.075f).SetEase(Ease.InOutSine);
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
