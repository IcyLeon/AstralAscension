using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public abstract class NavigationButton : MonoBehaviour
{
    private Button button;
    public event Action OnNavigationButtonClick;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(NavigationClick);
    }

    private void NavigationClick()
    {
        OnClick();
        OnNavigationButtonClick?.Invoke();
    }

    protected abstract void OnClick();
}
