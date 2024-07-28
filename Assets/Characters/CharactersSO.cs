using UnityEngine;

public class CharactersSO : ScriptableObject, IItem
{
    [field: SerializeField, Header("Base Character Infomation")] public string characterName { get; private set; }
    [field: SerializeField] public string characterDescription { get; private set; }
    [field: SerializeField] public Sprite characterIcon { get; private set; }
    [field: SerializeField] public ItemTypeSO characterTypeSO { get; private set; }

    public string GetDescription()
    {
        return characterDescription;
    }

    public Sprite GetIcon()
    {
        return characterIcon;
    }

    public string GetName()
    {
        return characterName;
    }
    public ItemTypeSO GetTypeSO()
    {
        return characterTypeSO;
    }

    public virtual Rarity GetRarity()
    {
        return Rarity.ONE_STAR;
    }

    public IItem GetInterfaceItemReference()
    {
        return this;
    }
}
