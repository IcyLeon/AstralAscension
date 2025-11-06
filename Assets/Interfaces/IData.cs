using System;
using UnityEngine;

public interface IData
{
    public string GetName();
    public ItemTypeSO GetTypeSO();
    public Sprite GetIcon();
    public string GetDescription();
    public ItemRaritySO GetRaritySO();
}

public interface IEntity : IData
{
    public event Action<IEntity> OnIEntityChanged;
    public bool IsNew();
    public void SetNewStatus(bool status);

}
