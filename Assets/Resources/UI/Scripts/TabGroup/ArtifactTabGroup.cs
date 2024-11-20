using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactTabGroup : TabGroup
{
    [SerializeField] private Scrollbar TabScrollBar;
    private Coroutine MovingScrollbar;

    protected override void Awake()
    {
        base.Awake();
        InitBarIndicator();
    }
    private void InitBarIndicator()
    {
        TabScrollBar.size = 1f / tabOptions.Length;
    }

    protected override void OnSelectedPanel(TabOption.TabEvents e)
    {
        base.OnSelectedPanel(e);

        int index = Array.IndexOf(tabOptions, e.TabOption);

        float targetValue = (float)index / (tabOptions.Length - 1);

        if (MovingScrollbar != null)
        {
            StopCoroutine(MovingScrollbar);
        }

        MovingScrollbar = StartCoroutine(MoveScrollBar(targetValue));
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

}
