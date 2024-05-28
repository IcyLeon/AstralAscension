using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterDataStat
{
    public Dictionary<ElementsSO, Elements> inflictElementList { get; private set; }
    public delegate void OnElementChange(Elements element);
    public event OnElementChange OnElementEnter;
    public event OnElementChange OnElementExit;

    public CharactersSO charactersSO { get; protected set; }
    public float maxHealth { get; protected set; }
    public float currentHealth { get; protected set; }

    public CharacterDataStat(CharactersSO charactersSO)
    {
        inflictElementList = new();

        this.charactersSO = charactersSO;

        currentHealth = maxHealth = 1000; // test
    }

    public void SetCurrentHealth(float health)
    {
        currentHealth = health;
    }

    private void UpdateElementList()
    {
        if (inflictElementList == null)
            return;

        for (int i = 0; i < inflictElementList.Count; i++)
        {
            inflictElementList.ElementAt(i).Value.Update();
        }

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

    public bool IsDead()
    {
        return false;
    }

    public virtual void Update()
    {
        UpdateElementList();
    }
}
