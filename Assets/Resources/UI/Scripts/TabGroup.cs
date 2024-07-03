using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    [SerializeField] private ScrollRect ScrollRect;
    private TabOption[] tabOptions;

    private void Awake()
    {
        tabOptions = GetComponentsInChildren<TabOption>();

        foreach(var tabOption in tabOptions)
        {
            tabOption.TabOptionClick += TabOption_TabOptionClick;
        }
    }

    private void TabOption_TabOptionClick(object sender, TabOption.TabEvents e)
    {
        foreach (var tabOption in tabOptions)
        {
            tabOption.Panel.gameObject.SetActive(false);
        }

        e.PanelRectTransform.gameObject.SetActive(true);

        ScrollRect.content = e.PanelRectTransform;
    }

    private void OnDestroy()
    {
        foreach (var tabOption in tabOptions)
        {
            tabOption.TabOptionClick -= TabOption_TabOptionClick;
        }
    }
}
