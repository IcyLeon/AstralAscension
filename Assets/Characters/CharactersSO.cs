using UnityEngine;

public class CharactersSO : ScriptableObject, IItem
{
    [field: SerializeField, Header("Base Character Infomation")] public string characterName { get; private set; }
    [field: SerializeField] public string characterDescription { get; private set; }
    [field: SerializeField] public Sprite characterIcon { get; private set; }
    [field: SerializeField] public ItemTypeSO characterTypeSO { get; private set; }

    public string GetItemDescription()
    {
        return characterDescription;
    }

    public Sprite GetItemIcon()
    {
        return characterIcon;
    }

    public string GetItemName()
    {
        return characterName;
    }
    public string GetItemType()
    {
        if (characterTypeSO == null)
            return "??";

        return characterTypeSO.ItemType;
    }

    public virtual Rarity GetItemRarity()
    {
        return Rarity.ONE_STAR;
    }
}
