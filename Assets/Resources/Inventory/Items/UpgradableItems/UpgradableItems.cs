using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradableItems : Item
{
    public CharactersSO equipByCharacter { get; private set; }
    public delegate void OnItemEquippedEvent(UpgradableItems UpgradableItems);
    public event OnItemEquippedEvent OnEquip;
    public event OnItemEquippedEvent OnUnEquip;

    public ItemEXPCostManagerSO expCostManagerSO { get; private set; }
    public int currentEXP { get; private set; }

    private int requiredEXP;

    public bool locked { get; private set; }
    public int maxLevel { get; private set; }

    public event Action OnUpgradeIEXP;
    public int level { get; private set; }

    public UpgradableItems(IItem iItem) : base(iItem)
    {
        level = 0;
        currentEXP = 0;
        OnCreateUpgradableItem();
        expCostManagerSO = InitItemEXPCostManagerSO();
        locked = false;
        maxLevel = expCostManagerSO.GetMaxLevel(GetRaritySO());
        UpdateEXPRequirement();
    }

    protected virtual void OnCreateUpgradableItem()
    {
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
        return level >= maxLevel;
    }

    private void Upgrade()
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
    private void UpdateEXPRequirement()
    {
        if (!expCostManagerSO)
            return;

        requiredEXP = expCostManagerSO.GetRequiredEXP(level, GetRaritySO());
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
        UpdateEXPRequirement();
    }

    public void AddEXP(int exp)
    {
        currentEXP = Mathf.Max(currentEXP + exp, 0);

        while (currentEXP >= requiredEXP)
        {
            if (IsMax())
            {
                currentEXP = 0;
                return;
            }
            currentEXP -= requiredEXP;
            Upgrade();
        }
    }
}
