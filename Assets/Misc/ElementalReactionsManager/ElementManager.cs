using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementManager
{
    private IEXP IEXPEntity;
    public Dictionary<ElementsSO, Elements> inflictElementList { get; private set; }
    public delegate void OnElementChange(Elements element);
    public event OnElementChange OnElementEnter;
    public event OnElementChange OnElementExit;

    public ElementManager(IEXP IEXP)
    {
        inflictElementList = new Dictionary<ElementsSO, Elements>();
        IEXPEntity = IEXP;

    }

    public void AddElement(ElementsSO elementSO, Elements elements)
    {
        inflictElementList.Add(elementSO, elements);
        OnElementEnter?.Invoke(elements);
    }

    public void RemoveElement(ElementsSO elementSO, Elements elements)
    {
        inflictElementList.Remove(elementSO);
        OnElementExit?.Invoke(elements);
    }
}