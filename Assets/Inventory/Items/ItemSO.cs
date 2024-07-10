using UnityEngine;

public enum Rarity
{
    ONE_STAR = 1,
    TWO_STAR,
    THREE_STAR,
    FOUR_STAR,
    FIVE_STAR,
}

public class ItemSO : ScriptableObject, IItem
{
    [field: SerializeField, Header("Base Item Information")] public string ItemName { get; private set; }
    [field: SerializeField] public Sprite ItemSprite { get; private set; }
    [field: SerializeField] public Rarity Rarity { get; private set; } = Rarity.ONE_STAR;
    [field: SerializeField, TextArea] public string ItemDescription { get; private set; }
    [field: SerializeField, Header("Item Type")] public ItemTypeSO ItemTypeSO { get; private set; }

    public string GetItemDescription()
    {
        return ItemDescription;
    }

    public Sprite GetItemIcon()
    {
        return ItemSprite;
    }

    public string GetItemName()
    {
        return ItemName;
    }

    public ItemTypeSO GetItemType()
    {
        return ItemTypeSO;
    }

    public Rarity GetItemRarity()
    {
        return Rarity;
    }

    public IItem GetInterfaceItemReference()
    {
        return this;
    }
}
