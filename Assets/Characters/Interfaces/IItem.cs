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

public interface IEntity : IItem
{
    public bool IsNew();
}

public interface IEXP
{
    public ItemEXPCostManagerSO GetExpCostSO();
    public int GetLevel();
    public int GetCurrentExp();
    public void AddCurrentExp(int exp);
}
