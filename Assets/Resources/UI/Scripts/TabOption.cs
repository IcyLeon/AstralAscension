using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class TabOption : MonoBehaviour, IPointerClickHandler
{
    public class TabEvents : EventArgs
    {
        public RectTransform PanelRectTransform;
        public Image TabOptionIconImage;
    }

    [field: SerializeField] public RectTransform Panel { get; private set; }
    [SerializeField] private Image IconImage;
    [SerializeField] private Image BackgroundIconImage;
    [SerializeField] private Color32 DefaultIconColor;

    public event EventHandler<TabEvents> TabOptionClick;

    public void ResetTab()
    {
        if (BackgroundIconImage != null)
            BackgroundIconImage.gameObject.SetActive(false);

        if (IconImage != null)
            IconImage.color = DefaultIconColor;
    }

    public void Select()
    {
        TabOptionClick?.Invoke(this, new TabEvents
        {
            PanelRectTransform = Panel,
            TabOptionIconImage = IconImage,
        });

        if (BackgroundIconImage != null)
            BackgroundIconImage.gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Select();
    }
}
