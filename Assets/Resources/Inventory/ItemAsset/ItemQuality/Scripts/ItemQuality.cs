using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[DisallowMultipleComponent]
public abstract class ItemQuality : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image RaycastImage { get; private set; }
    [SerializeField] private ItemQualityVisual itemQualityVisual;
    public event Action<ItemQuality> OnItemQualitySelect;
    public IData iData { get; private set; }

    protected virtual void Awake()
    {
        RaycastImage = itemQualityVisual.GetComponent<Image>();
    }

    public virtual void SetIItem(IData IData)
    {
        iData = IData;
        itemQualityVisual.SetIItem(iData);
    }

    protected virtual void OnDestroy()
    {
        OnItemQualitySelect = null;
    }

    protected void UpdateDisplayText(string txt)
    {
        itemQualityVisual.UpdateDisplayText(txt);
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
