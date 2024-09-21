using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterDataStat : IEntity
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

    public EffectManager effectManager { get; }
    public ArtifactEffectManager artifactEffectManager { get; private set; }

    public DamageableEntitySO damageableEntitySO { get; protected set; }
    private float maxHealth;
    private float currentHealth;
    public int level { get; private set; }

    public CharacterDataStat(CharactersSO charactersSO)
    {
        effectManager = new();
        artifactEffectManager = new(this, effectManager);

        inflictElementList = new();
        equippeditemList = new();
        damageableEntitySO = charactersSO as DamageableEntitySO;

        level = 1;
        currentHealth = maxHealth = 1000;
    }

    public void RemoveEquipItem(ItemTypeSO itemTypeSO)
    {
        IItem item = GetItem(itemTypeSO);

        if (item == null)
            return;

        equippeditemList.Remove(itemTypeSO);
        UnequipItem(item);
        OnItemChanged?.Invoke(item);
        OnItemRemove?.Invoke(item);
    }

    private void UnequipItem(IItem item)
    {
        UpgradableItems upgradableItem = item as UpgradableItems;

        if (upgradableItem == null)
            return;

        upgradableItem.SetEquip(null);
    }

    public void AddEquipItem(Item item)
    {
        if (item == null)
            return;

        ItemTypeSO itemTypeSO = item.GetTypeSO();

        if (GetItem(itemTypeSO) != null)
        {
            RemoveEquipItem(itemTypeSO);
        }

        equippeditemList.Add(item.GetTypeSO(), item);
        OnItemChanged?.Invoke(item);
        OnItemAdd?.Invoke(item);
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
}
