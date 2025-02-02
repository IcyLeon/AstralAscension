using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradableItems : Item, IEXP
{
    public CharactersSO equipByCharacter { get; private set; }
    public delegate void OnItemEquippedEvent(UpgradableItems UpgradableItems);
    public event OnItemEquippedEvent OnEquip;
    public event OnItemEquippedEvent OnUnEquip;

    private ItemEXPCostManagerSO expCostManagerSO;
    private int totalEXP;
    private int currentEXP;
    public bool locked { get; private set; }
    protected int maxLevel;

    public event Action OnUpgradeIEXP;
    private int level;

    public UpgradableItems(IItem iItem) : base(iItem)
    {
        level = 0;
        OnCreateUpgradableItem();
        ResetEXP();
        expCostManagerSO = InitItemEXPCostManagerSO();
        locked = false;
        maxLevel = GetExpCostSO().GetMaxLevel(GetRaritySO());
    }

    protected virtual void OnCreateUpgradableItem()
    {
    }

    public int GetMaxLevel()
    {
        return maxLevel;
    }

    protected virtual ItemEXPCostManagerSO InitItemEXPCostManagerSO()
    {
        return null;
    }

    public void Unequip()
    {
        if (equipByCharacter == null)
            return;

        equipByCharacter = null;
        OnUnEquip?.Invoke(this);
        OnEquip = OnUnEquip = null;
        CallOnItemChanged();
    }

    public void Equip(CharactersSO charactersSO)
    {
        if (charactersSO == null)
            return;

        equipByCharacter = charactersSO;
        OnEquip?.Invoke(this);
        CallOnItemChanged();
    }

    public bool IsMax()
    {
        return GetLevel() >= GetMaxLevel();
    }

    public void Upgrade()
    {
        if (IsMax())
            return;

        level++;
        UpgradeItemAction();
        CallOnItemChanged();
        OnUpgradeIEXP?.Invoke();
    }

    public override void AddAmount(int amount)
    {

    }

    public override bool IsStackable()
    {
        return false;
    }

    public void SetLockStatus(bool locked)
    {
        this.locked = locked;
        CallOnItemChanged();
    }

    protected virtual void UpgradeItemAction()
    {
        if (!IsMax())
            return;

        ResetEXP();
    }

    public int GetLevel()
    {
        return level;
    }

    public int GetCurrentExp()
    {
        return currentEXP;
    }

    public void ResetEXP()
    {
        currentEXP = 0;
    }

    public ItemEXPCostManagerSO GetExpCostSO()
    {
        return expCostManagerSO;
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
