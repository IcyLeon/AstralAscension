using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ToggleGroup))]
public class TabGroup : MonoBehaviour
{
    protected TabOption[] tabOptions;
    public event Action<TabOption> OnTabGroupChanged;
    public ToggleGroup toggleGroup { get; private set; }


    protected virtual void Awake()
    {
        tabOptions = GetComponentsInChildren<TabOption>(true);
        toggleGroup = GetComponent<ToggleGroup>();
    }

    private void Start()
    {
        foreach (var tabOption in tabOptions)
        {
            tabOption.TabOptionSelect += TabOption_TabOptionSelect;
        }

        if (tabOptions.Length > 0)
            tabOptions[0].toggle.isOn = true;
    }

    protected virtual void OnSelectedPanel(TabOption TabOption)
    {
        OnTabGroupChanged?.Invoke(TabOption);
    }

    private void TabOption_TabOptionSelect(TabOption TabOption)
    {
        OnSelectedPanel(TabOption);
    }

    private void OnDestroy()
    {
        foreach (var tabOption in tabOptions)
        {
            tabOption.TabOptionSelect -= TabOption_TabOptionSelect;
        }
    }
}
