using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradableItems : Item, IEXP
{
    public CharactersSO equipByCharacter { get; private set; }
    private ItemEXPCostManagerSO expCostManagerSO;
    private int currentEXP;
    public bool locked { get; private set; }
    protected int maxLevel;

    public event EventHandler OnUpgradeIEXP;

    public UpgradableItems(IItem iItem) : base(iItem)
    {
        OnCreateUpgradableItem();
        expCostManagerSO = InitItemEXPCostManagerSO();
        locked = false;
        maxLevel = 20;
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

    public void SetEquip(CharactersSO charactersSO)
    {
        equipByCharacter = charactersSO;
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

        amount++;
        UpgradeItemAction();
        CallOnItemChanged();
        OnUpgradeIEXP?.Invoke(this, EventArgs.Empty);
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
        return amount;
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

    public void SetCurrentExp(int exp)
    {
        currentEXP = exp;
        currentEXP = Mathf.Max(currentEXP, 0);
    }

    public IEntity GetIEntity()
    {
        return this;
    }
}
