using UnityEngine;

public interface IItem
{
    public string GetItemName();
    public Sprite GetItemIcon();
    public string GetItemDescription();
    public Rarity GetItemRarity();
}
