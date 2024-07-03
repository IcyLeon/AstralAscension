using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[DisallowMultipleComponent]
public class TabOption : MonoBehaviour, IPointerClickHandler
{
    public class TabEvents : EventArgs
    {
        public RectTransform PanelRectTransform;
    }

    [field: SerializeField] public RectTransform Panel { get; private set; }
    public event EventHandler<TabEvents> TabOptionClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        TabOptionClick?.Invoke(this, new TabEvents { PanelRectTransform = Panel });
    }
}
