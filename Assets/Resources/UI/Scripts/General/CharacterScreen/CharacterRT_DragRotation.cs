using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[DisallowMultipleComponent]
public class CharacterRT_DragRotation : MonoBehaviour, IScrollHandler, IDragHandler
{
    public event Action<float> OnZoom;
    public event Action<Vector2> OnMove;

    public void OnScroll(PointerEventData eventData)
    {
        OnZoom?.Invoke(eventData.scrollDelta.y);
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnMove?.Invoke(eventData.delta);
    }
}
