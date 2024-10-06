using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradableItems : Item, IEXP
{
    public CharactersSO equipByCharacter { get; private set; }
    private ItemEXPCostManagerSO expCostManagerSO;
    private int currentEXP;
    public bool locked { get; private set; }
    public int maxLevel { get; protected set; }
    public UpgradableItems(IItem iItem) : base(iItem)
    {
        expCostManagerSO = InitItemEXPCostManagerSO();
        locked = false;

        maxLevel = 20;
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
        return amount >= maxLevel;
    }

    public void Upgrade()
    {
        if (IsMax())
        {
            return;
        }

        amount++;
        UpgradeItemAction();
        CallOnItemChanged();
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

    public void AddCurrentExp(int exp)
    {
        currentEXP += exp;
        currentEXP = Mathf.Max(currentEXP, 0);
    }
}
