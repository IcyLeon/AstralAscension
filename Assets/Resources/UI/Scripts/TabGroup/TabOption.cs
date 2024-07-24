using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class TabOption : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public class TabEvents : EventArgs
    {
        public RectTransform PanelRectTransform;
        public Image TabOptionIconImage;
    }

    [field: SerializeField] public RectTransform Panel { get; private set; }
    [SerializeField] private CanvasGroup IconCanvasGroup;
    [SerializeField] private Image IconImage;
    [SerializeField] private Image BackgroundIconImage;

    public event EventHandler<TabEvents> TabOptionClick;
    private TabGroup tabGroup;

    public void SetTabGroup(TabGroup tabGroup)
    {
        this.tabGroup = tabGroup;
    }

    public void ResetTab()
    {
        ResetCanvasAlpha();

        if (BackgroundIconImage != null)
            BackgroundIconImage.gameObject.SetActive(false);

        if (IconImage != null)
            IconImage.color = Color.white;

        Panel.gameObject.SetActive(false);
    }

    public void Select()
    {
        TabOptionClick?.Invoke(this, new TabEvents
        {
            PanelRectTransform = Panel,
            TabOptionIconImage = IconImage,
        });

        SelectCanvasAlpha();

        if (BackgroundIconImage != null)
            BackgroundIconImage.gameObject.SetActive(true);

        Panel.gameObject.SetActive(true);
    }

    private void ResetCanvasAlpha()
    {
        IconCanvasGroup.alpha = 0.35f;
    }

    private void SelectCanvasAlpha()
    {
        IconCanvasGroup.alpha = 1f;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        Select();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tabGroup.selectedTabOption == this)
            return;

        SelectCanvasAlpha();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tabGroup.selectedTabOption == this)
            return;

        ResetCanvasAlpha();
    }
}
