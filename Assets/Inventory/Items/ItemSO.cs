using UnityEngine;

public enum Rarity
{
    ONE_STAR,
    TWO_STAR,
    THREE_STAR,
    FOUR_STAR,
    FIVE_STAR,
}

public class ItemSO : ScriptableObject, IItem
{
    [field: SerializeField, Header("Base Item Information")] public string ItemName { get; private set; }
    [field: SerializeField] public Sprite ItemSprite { get; private set; }
    [field: SerializeField] public ItemRaritySO ItemRaritySO { get; private set; }
    [field: SerializeField, TextArea] public string ItemDescription { get; private set; }
    [field: SerializeField, Header("Item Type")] public ItemTypeSO ItemTypeSO { get; private set; }

    public string GetDescription()
    {
        return ItemDescription;
    }

    public Sprite GetIcon()
    {
        return ItemSprite;
    }

    public string GetName()
    {
        return ItemName;
    }

    public ItemTypeSO GetTypeSO()
    {
        return ItemTypeSO;
    }

    public Rarity GetRarity()
    {
        if (ItemRaritySO == null)
        {
            return Rarity.ONE_STAR;
        }

        return ItemRaritySO.Rarity;
    }

    public IItem GetIItem()
    {
        return this;
    }
}
