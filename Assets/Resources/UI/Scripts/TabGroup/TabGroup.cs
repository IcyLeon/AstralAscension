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
    public event EventHandler<TabOption.TabEvents> OnTabGroupChanged;

    protected virtual void Awake()
    {
        tabOptions = GetComponentsInChildren<TabOption>(true);

        if (tabOptions == null)
            return;

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

    protected virtual void OnSelectedPanel(TabOption tabOption, TabOption.TabEvents e)
    {
        if (e.TabOptionIconImage != null)
            e.TabOptionIconImage.color = SelectedTabColor;

        OnTabGroupChanged?.Invoke(this, new TabOption.TabEvents
        {
            PanelRectTransform = tabOption.Panel,
            TabOptionIconImage = e.TabOptionIconImage
        });
    }

    private void TabOption_TabOptionClick(object sender, TabOption.TabEvents e)
    {
        ResetAllTabOption();
        selectedTabOption = sender as TabOption;
        OnSelectedPanel(selectedTabOption, e);
    }

    private void OnDestroy()
    {
        foreach (var tabOption in tabOptions)
        {
            tabOption.TabOptionClick -= TabOption_TabOptionClick;
        }
    }
}
