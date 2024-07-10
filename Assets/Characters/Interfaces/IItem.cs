using UnityEngine;

public interface IItem
{
    public string GetItemName();
    public ItemTypeSO GetItemType();
    public Sprite GetItemIcon();
    public string GetItemDescription();
    public Rarity GetItemRarity();

    public IItem GetInterfaceItemReference();
}
