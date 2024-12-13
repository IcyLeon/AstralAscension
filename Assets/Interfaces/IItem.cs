using System;
using UnityEngine;

public interface IItem
{
    public string GetName();
    public ItemTypeSO GetTypeSO();
    public Sprite GetIcon();
    public string GetDescription();
    public ItemRaritySO GetRaritySO();
    public IItem GetIItem();
}

public interface IEntity : IItem
{
    public event Action<IEntity> OnIEntityChanged;
    public bool IsNew();
    public void SetNewStatus(bool status);
}

public interface IEXP
{
    public event Action OnUpgradeIEXP;
    public ItemEXPCostManagerSO GetExpCostSO();
    public int GetLevel();
    public int GetMaxLevel();
    public int GetCurrentExp();
    public int GetTotalExp();
    public void AddExp(int exp);
    public void RemoveExp(int exp);
    public IEntity GetIEntity();
    public void Upgrade();
}
