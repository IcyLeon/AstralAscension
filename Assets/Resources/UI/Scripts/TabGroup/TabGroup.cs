using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ToggleGroup))]
public class TabGroup : MonoBehaviour
{
    public TabOption[] tabOptions { get; private set; }
    public event Action<TabOption> OnTabOptionChanged;
    public ToggleGroup toggleGroup { get; private set; }
    public TabOption currentTabOption { get; private set; }

    private void Awake()
    {
        tabOptions = GetComponentsInChildren<TabOption>(true);
        toggleGroup = GetComponent<ToggleGroup>();

        foreach (var tabOption in tabOptions)
        {
            tabOption.OnTabOptionSelect += TabOption_TabOptionSelect;
        }
    }

    private void Start()
    {
        InitTabOptions();
    }

    private void InitTabOptions()
    {
        if (toggleGroup.AnyTogglesOn())
            return;

        if (tabOptions.Length > 0)
        {
            tabOptions[0].toggle.isOn = true;
        }
    }

    private void TabOption_TabOptionSelect(TabOption TabOption)
    {
        currentTabOption = TabOption;
        OnTabOptionChanged?.Invoke(TabOption);
    }

    private void OnDestroy()
    {
        foreach (var tabOption in tabOptions)
        {
            tabOption.OnTabOptionSelect -= TabOption_TabOptionSelect;
        }
    }
}
