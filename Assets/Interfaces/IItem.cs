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
