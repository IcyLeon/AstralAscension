using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class CharacterDataStat : IEntity
{
    public Dictionary<ElementsSO, Elements> inflictElementList { get; private set; }
    public delegate void OnElementChange(Elements element);
    public event OnElementChange OnElementEnter;
    public event OnElementChange OnElementExit;

    public DamageableEntitySO damageableEntitySO { get; protected set; }
    private float maxHealth;
    private float currentHealth;
    public int level { get; private set; }

    public CharacterDataStat(CharactersSO charactersSO)
    {
        inflictElementList = new();

        damageableEntitySO = charactersSO as DamageableEntitySO;

        level = 1;
        currentHealth = maxHealth = 1000; // test
    }

    public void SetCurrentHealth(float health)
    {
        currentHealth = health;
    }
    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
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

    public string GetItemName()
    {
        return damageableEntitySO.GetItemName();
    }

    public ItemTypeSO GetItemType()
    {
        return damageableEntitySO.GetItemType();
    }

    public Sprite GetItemIcon()
    {
        return damageableEntitySO.GetItemIcon();
    }

    public string GetItemDescription()
    {
        return damageableEntitySO.GetItemDescription();
    }

    public Rarity GetItemRarity()
    {
        return damageableEntitySO.GetItemRarity();
    }

    public IItem GetInterfaceItemReference()
    {
        return damageableEntitySO.GetInterfaceItemReference();
    }

    public bool IsNew()
    {
        return false;
    }
}
