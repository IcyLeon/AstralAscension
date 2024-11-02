using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterDataStat : IEntity, IEXP
{
    public Dictionary<ItemTypeSO, IItem> equippeditemList { get; } // equipped items character
    public delegate void OnItemEquippedEvent(IItem IItem);
    public event OnItemEquippedEvent OnItemChanged;
    public event OnItemEquippedEvent OnItemAdd;
    public event OnItemEquippedEvent OnItemRemove;


    public Dictionary<ElementsSO, Elements> inflictElementList { get; private set; }
    public delegate void OnElementChange(Elements element);
    public event OnElementChange OnElementEnter;
    public event OnElementChange OnElementExit;
    public event EventHandler OnUpgradeIEXP;
    public event EventHandler<IEntityEvents> OnIEntityChanged;

    public EffectManager effectManager { get; }
    public ArtifactEffectManager artifactEffectManager { get; private set; }

    public DamageableEntitySO damageableEntitySO { get; protected set; }
    private float maxHealth;
    private float currentHealth;
    private int level;
    private int maxlevel;
    private int currentEXP;

    public CharacterDataStat(CharactersSO charactersSO)
    {
        effectManager = new();
        artifactEffectManager = new(this, effectManager);

        inflictElementList = new();
        equippeditemList = new();
        damageableEntitySO = charactersSO as DamageableEntitySO;

        level = 1;
        maxlevel = 20;
        currentEXP = 0;
        currentHealth = maxHealth = 1000;
    }

    public void UnequipItem(ItemTypeSO itemTypeSO)
    {
        IItem existingiItem = GetItem(itemTypeSO);

        UpgradableItems upgradableItem = existingiItem as UpgradableItems;

        if (upgradableItem == null)
            return;

        equippeditemList.Remove(itemTypeSO);
        upgradableItem.SetEquip(null);
        OnItemRemove?.Invoke(existingiItem);
        OnItemChanged?.Invoke(existingiItem);
    }

    public void EquipItem(IItem iItem)
    {
        if (iItem == null)
            return;

        UnequipItem(iItem.GetTypeSO());

        UpgradableItems upgradableItem = iItem as UpgradableItems;

        if (upgradableItem == null)
            return;

        equippeditemList.Add(iItem.GetTypeSO(), iItem);
        upgradableItem.SetEquip(damageableEntitySO);
        OnItemAdd?.Invoke(iItem);
        OnItemChanged?.Invoke(iItem);
    }

    public IItem GetItem(ItemTypeSO itemTypeSO)
    {
        if (itemTypeSO != null && equippeditemList.TryGetValue(itemTypeSO, out IItem item))
        {
            return item;
        }

        return null;
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
        return damageableEntitySO.GetName();
    }

    public ItemTypeSO GetTypeSO()
    {
        return damageableEntitySO.GetTypeSO();
    }

    public Sprite GetIcon()
    {
        return damageableEntitySO.GetIcon();
    }

    public string GetDescription()
    {
        return damageableEntitySO.GetDescription();
    }

    public Rarity GetRarity()
    {
        return damageableEntitySO.GetRarity();
    }

    public IItem GetInterfaceItemReference()
    {
        return damageableEntitySO.GetInterfaceItemReference();
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

    public void SetCurrentExp(int exp)
    {
    }

    public void Upgrade()
    {
        OnUpgradeIEXP?.Invoke(this, EventArgs.Empty);
    }

    public void SetNewStatus(bool status)
    {
    }

    public IEntity GetIEntity()
    {
        return this;
    }
}
