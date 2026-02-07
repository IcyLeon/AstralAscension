using UnityEngine;

public enum Rarity
{
    ONE_STAR = 1,
    TWO_STAR,
    THREE_STAR,
    FOUR_STAR,
    FIVE_STAR,
}

public abstract class ItemSO : ScriptableObject, IData
{
    [Header("Base Item Information")]
    [SerializeField] public string Name;
    [SerializeField] private Sprite Icon;
    [SerializeField] public ItemRaritySO RaritySO;
    [SerializeField] public ItemTypeSO ItemTypeSO;
    [TextArea]
    [SerializeField] public string Description;

    public string GetDescription()
    {
        return Description;
    }

    public Sprite GetIcon()
    {
        return Icon;
    }

    public string GetName()
    {
        return Name;
    }

    public ItemTypeSO GetTypeSO()
    {
        return ItemTypeSO;
    }

    public ItemRaritySO GetRaritySO()
    {
        return RaritySO;
    }

    public virtual Item CreateItem()
    {
        return new Item(this);
    }
}
