using UnityEngine;

public enum Rarity
{
    ONE_STAR = 1,
    TWO_STAR,
    THREE_STAR,
    FOUR_STAR,
    FIVE_STAR,
}

public class ItemSO : ScriptableObject
{
    [Header("Base Item Description")]
    public string ItemName;
    public Sprite ItemSprite;
    public Rarity Rarity;
}
