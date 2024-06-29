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
    [field: SerializeField, Header("Base Item Information")] public string ItemName { get; private set; }
    [field: SerializeField] public Sprite ItemSprite { get; private set; }
    [field: SerializeField, TextArea] public string ItemDesc { get; private set; }
    [field: SerializeField] public Rarity Rarity { get; private set; } = Rarity.ONE_STAR;
}
