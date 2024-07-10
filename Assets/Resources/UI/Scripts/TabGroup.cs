using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    [SerializeField] private ScrollRect ScrollRect;
    [SerializeField] private Color32 SelectedTabColor;
    protected TabOption[] tabOptions;
    protected virtual void Awake()
    {
        tabOptions = GetComponentsInChildren<TabOption>(true);

        foreach(var tabOption in tabOptions)
        {
            tabOption.TabOptionClick += TabOption_TabOptionClick;
        }
    }

    private void Start()
    {
        if (tabOptions.Length > 0)
            tabOptions[0].Select();
    }

    protected virtual void OnSelectedPanel(object sender, TabOption.TabEvents e)
    {
        foreach (var tabOption in tabOptions)
        {
            tabOption.Panel.gameObject.SetActive(false);
            tabOption.ResetTab();
        }

        ActivePanel(e);

        ScrollRect.content = e.PanelRectTransform;
    }

    private void TabOption_TabOptionClick(object sender, TabOption.TabEvents e)
    {
        OnSelectedPanel(sender, e);
    }

    private void ActivePanel(TabOption.TabEvents e)
    {
        e.TabOptionIconImage.color = SelectedTabColor;
        e.PanelRectTransform.gameObject.SetActive(true);
    }


    private void OnDestroy()
    {
        foreach (var tabOption in tabOptions)
        {
            tabOption.TabOptionClick -= TabOption_TabOptionClick;
        }
    }
}
