using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragEvent : EventArgs
{
    public PointerEventData eventData;
}

public class DragnDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public event EventHandler<DragEvent> OnDragEvent;

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnDragEvent?.Invoke(this, new DragEvent { eventData = eventData });
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }
}
