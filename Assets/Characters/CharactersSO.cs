using UnityEngine;

public class CharactersSO : ScriptableObject, IItem
{
    [field: SerializeField, Header("Base Character Infomation")] public string CharacterName { get; private set; }
    [field: SerializeField] public string CharacterDescription { get; private set; }
    [field: SerializeField] public Sprite CharacterIcon { get; private set; }
    [field: SerializeField] public ItemTypeSO CharacterTypeSO { get; private set; }

    [field: SerializeField] public GameObject CharacterPrefab { get; private set; }

    public string GetDescription()
    {
        return CharacterDescription;
    }

    public Sprite GetIcon()
    {
        return CharacterIcon;
    }

    public string GetName()
    {
        return CharacterName;
    }
    public ItemTypeSO GetTypeSO()
    {
        return CharacterTypeSO;
    }

    public virtual Rarity GetRarity()
    {
        return Rarity.ONE_STAR;
    }

    public IItem GetIItem()
    {
        return this;
    }
}
