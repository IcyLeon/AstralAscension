using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterDataStat : IEntity, IEXP
{    
    public EffectManager effectManager { get; }
    public CharacterInventory characterInventory { get; private set; }
    public Dictionary<ElementsSO, Elements> inflictElementList { get; private set; }
    public delegate void OnElementChange(Elements element);
    public event OnElementChange OnElementEnter;
    public event OnElementChange OnElementExit;
    public event Action OnUpgradeIEXP;
    public event Action<IEntity> OnIEntityChanged;



    public DamageableEntitySO damageableEntitySO { get; protected set; }
    private float maxHealth;
    private float currentHealth;
    private int level;
    private int maxlevel;
    private int currentEXP;
    private int totalEXP;

    public CharacterDataStat(CharactersSO charactersSO)
    {
        characterInventory = new(charactersSO);
        effectManager = new(characterInventory);
        inflictElementList = new();
        damageableEntitySO = charactersSO as DamageableEntitySO;

        level = 1;
        maxlevel = 20;
        currentEXP = 0;
        currentHealth = maxHealth = 1000;
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

    public string GetName()
    {
        return GetIItem().GetName();
    }

    public ItemTypeSO GetTypeSO()
    {
        return GetIItem().GetTypeSO();
    }

    public Sprite GetIcon()
    {
        return GetIItem().GetIcon();
    }

    public string GetDescription()
    {
        return GetIItem().GetDescription();
    }

    public ItemRaritySO GetRaritySO()
    {
        return GetIItem().GetRaritySO();
    }

    public IItem GetIItem()
    {
        return GetIItem().GetIItem();
    }

    public bool IsNew()
    {
        return false;
    }

    public virtual void OnDestroy()
    {
        effectManager.OnDestroy();
    }

    public int GetLevel()
    {
        return level;
    }

    public int GetMaxLevel()
    {
        return maxlevel;
    }

    public int GetCurrentExp()
    {
        return currentEXP;
    }

    public ItemEXPCostManagerSO GetExpCostSO()
    {
        return null;
    }

    public void Upgrade()
    {
        OnUpgradeIEXP?.Invoke();
    }

    public void SetNewStatus(bool status)
    {
    }

    public IEntity GetIEntity()
    {
        return this;
    }

    public int GetTotalExp()
    {
        return totalEXP;
    }

    public void AddExp(int exp)
    {
        currentEXP += exp;
        totalEXP += exp;
        currentEXP = Mathf.Max(currentEXP, 0);
    }

    public void RemoveExp(int exp)
    {
        currentEXP -= exp;
        currentEXP = Mathf.Max(currentEXP, 0);
    }
}
