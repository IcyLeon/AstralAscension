using UnityEngine;

public enum Rarity
{
    ONE_STAR,
    TWO_STAR,
    THREE_STAR,
    FOUR_STAR,
    FIVE_STAR,
}

public abstract class ItemSO : ScriptableObject, IItem
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

    public ItemRaritySO GetRaritySO()
    {
        return ItemRaritySO;
    }

    public IItem GetIItem()
    {
        return this;
    }

    public virtual Item CreateItem()
    {
        return null;
    }
}
