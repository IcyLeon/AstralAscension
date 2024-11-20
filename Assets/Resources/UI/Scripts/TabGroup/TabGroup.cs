using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    [SerializeField] private Color32 SelectedTabColor;
    protected TabOption[] tabOptions;

    public TabOption selectedTabOption { get; private set; }
    public event Action<TabOption.TabEvents> OnTabGroupChanged;

    protected virtual void Awake()
    {
        tabOptions = GetComponentsInChildren<TabOption>(true);

        foreach(var tabOption in tabOptions)
        {
            tabOption.TabOptionClick += TabOption_TabOptionClick;
            tabOption.SetTabGroup(this);
        }
        ResetAllTabOption();
    }

    private void Start()
    {
        if (tabOptions.Length > 0)
            tabOptions[0].Select();
    }

    private void ResetAllTabOption()
    {
        foreach (var tabOption in tabOptions)
        {
            tabOption.ResetTab();
        }
    }

    protected virtual void OnSelectedPanel(TabOption.TabEvents e)
    {
        if (e.TabOptionIconImage != null)
            e.TabOptionIconImage.color = SelectedTabColor;

        selectedTabOption = e.TabOption;

        OnTabGroupChanged?.Invoke(new TabOption.TabEvents
        {
            PanelRectTransform = e.PanelRectTransform,
            TabOptionIconImage = e.TabOptionIconImage
        });
    }

    private void TabOption_TabOptionClick(TabOption.TabEvents e)
    {
        ResetAllTabOption();
        OnSelectedPanel(e);
    }

    private void OnDestroy()
    {
        foreach (var tabOption in tabOptions)
        {
            tabOption.TabOptionClick -= TabOption_TabOptionClick;
        }
    }
}
