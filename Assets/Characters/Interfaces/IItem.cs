using System;
using UnityEngine;

public interface IItem
{
    public string GetName();
    public ItemTypeSO GetTypeSO();
    public Sprite GetIcon();
    public string GetDescription();
    public Rarity GetRarity();

    public IItem GetInterfaceItemReference();
}

public class IEntityEvents : EventArgs
{
    public IEntity iEntity;
}
public interface IEntity : IItem
{
    public event EventHandler<IEntityEvents> OnIEntityChanged;
    public bool IsNew();
    public void SetNewStatus(bool status);
}

public interface IEXP
{
    public event EventHandler OnUpgradeIEXP;
    public ItemEXPCostManagerSO GetExpCostSO();
    public int GetLevel();
    public int GetMaxLevel();
    public int GetCurrentExp();
    public void SetCurrentExp(int exp);
    public IEntity GetIEntity();
    public void Upgrade();
}
