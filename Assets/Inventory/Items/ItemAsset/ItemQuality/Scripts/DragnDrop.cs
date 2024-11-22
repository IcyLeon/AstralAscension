using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragnDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public event Action<PointerEventData> OnBeginDragEvent;
    public event Action<PointerEventData> OnDragEvent;
    public event Action<PointerEventData> OnEndDragEvent;
    private Transform originalParent;
    private RectTransform RT;
    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        RT = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(canvas.transform);
        OnBeginDragEvent?.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        RT.anchoredPosition += eventData.delta / canvas.scaleFactor;
        
        OnDragEvent?.Invoke(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originalParent);
        OnEndDragEvent?.Invoke(eventData);
    }
}
