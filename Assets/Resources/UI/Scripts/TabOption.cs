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
    private Color32 DefaultIconColor;

    public event EventHandler<TabEvents> TabOptionClick;

    private void Awake()
    {
        DefaultIconColor = IconImage.color;
    }

    public void ResetTab()
    {
        if (BackgroundIconImage != null)
            BackgroundIconImage.gameObject.SetActive(false);

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
