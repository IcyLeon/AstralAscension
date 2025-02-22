using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TabGroup))]
public class TabIndicator : MonoBehaviour
{
    private TabGroup tabGroup;
    [SerializeField] private Scrollbar TabScrollBar;
    private Coroutine MovingScrollbar;

    private void Awake()
    {
        tabGroup = GetComponent<TabGroup>();
    }

    private void TabGroup_OnTabOptionChanged(TabOption TabOption)
    {
        if (TabOption == null)
            return;

        int index = Array.IndexOf(tabGroup.tabOptions, TabOption);

        float targetValue = (float)index / (tabGroup.tabOptions.Length - 1);

        if (MovingScrollbar != null)
        {
            StopCoroutine(MovingScrollbar);
        }

        MovingScrollbar = StartCoroutine(MoveScrollBar(targetValue));
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        tabGroup.OnTabOptionChanged += TabGroup_OnTabOptionChanged;
        TabGroup_OnTabOptionChanged(tabGroup.currentTabOption);
        InitBarIndicator();
    }

    private void UnsubscribeEvents()
    {
        tabGroup.OnTabOptionChanged -= TabGroup_OnTabOptionChanged;
    }


    private void InitBarIndicator()
    {
        TabScrollBar.size = 1f / tabGroup.tabOptions.Length;
    }

    private IEnumerator MoveScrollBar(float target)
    {
        float elapsedTime = 0f;
        float animationDuration = 0.15f;

        while (elapsedTime < animationDuration)
        {
            TabScrollBar.value = Mathf.Lerp(TabScrollBar.value, target, elapsedTime / animationDuration);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
        TabScrollBar.value = target;
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }
}
