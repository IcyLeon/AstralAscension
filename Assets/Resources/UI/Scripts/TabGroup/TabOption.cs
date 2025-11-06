using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Toggle))]
public class TabOption : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Serializable]
    public class CustomizeableTabOption
    {
        [field: SerializeField] public Color32 SelectedColor { get; private set; }
        [field: SerializeField] public Color32 UnSelectedColor { get; private set; }
    }

    [field: SerializeField] public RectTransform Panel { get; private set; }
    [SerializeField] private Graphic BackgroundGraphic;
    [SerializeField] private CustomizeableTabOption CustomizeableIconTabOption;
    [SerializeField] private Color32 HoverColor;
    public event Action<TabOption> OnTabOptionSelect;
    public event Action OnTabOptionHover;
    public event Action OnTabOptionExit;

    private TabGroup tabGroup;
    public Toggle toggle { get; private set; }

    private void Awake()
    {
        tabGroup = GetComponentInParent<TabGroup>();
        toggle = GetComponent<Toggle>();
    }

    private void Start()
    {
        toggle.group = tabGroup.toggleGroup;
        toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(toggle);
        });

        ToggleValueChanged(toggle);
    }

    private void ToggleValueChanged(Toggle toggle)
    {
        Panel.gameObject.SetActive(toggle.isOn);

        if (toggle.isOn)
        {
            OnSelected();
            OnClick();
            OnTabOptionSelect?.Invoke(this);
            return;
        }

        OnDeSelected();
    }

    public void OnClick()
    {
        toggle.isOn = true;
    }

    private void OnSelected()
    {
        OnSelectedBackground();
        OnSelectedIcon();
    }

    private void OnDeSelected()
    {
        OnDeselectedBackground();
        OnUnSelectedIcon();
    }

    private void OnSelectedBackground()
    {
        var Color = BackgroundGraphic.color;
        Color.a = 1f;
        BackgroundGraphic.color = Color;
    }

    private void OnDeselectedBackground()
    {
        var Color = BackgroundGraphic.color;
        Color.a = 0f;
        BackgroundGraphic.color = Color;
    }

    private void OnHoverIcon()
    {
        toggle.targetGraphic.color = HoverColor;
    }

    private void OnSelectedIcon()
    {
        toggle.targetGraphic.color = CustomizeableIconTabOption.SelectedColor;
    }

    private void OnUnSelectedIcon()
    {
        toggle.targetGraphic.color = CustomizeableIconTabOption.UnSelectedColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (IsCurrentSelected())
            return;

        OnHoverIcon();
        OnTabOptionHover?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (IsCurrentSelected())
            return;

        OnUnSelectedIcon();
        OnTabOptionExit?.Invoke();
    }

    private bool IsCurrentSelected()
    {
        return toggle.isOn;
    }
}
