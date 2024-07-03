using UnityEngine;

public interface IItem
{
    public string GetItemName();
    public string GetItemType();
    public Sprite GetItemIcon();
    public string GetItemDescription();
    public Rarity GetItemRarity();
}
