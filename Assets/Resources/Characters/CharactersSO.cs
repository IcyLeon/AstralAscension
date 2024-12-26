using UnityEngine;

public class CharactersSO : ScriptableObject, IItem
{
    [field: SerializeField, Header("Base Character Infomation")] public string CharacterName { get; private set; }
    [field: SerializeField] public string CharacterDescription { get; private set; }
    [field: SerializeField] public Sprite CharacterIcon { get; private set; }
    [field: SerializeField] public ItemTypeSO CharacterTypeSO { get; private set; }
    [field: SerializeField] public ItemRaritySO CharacterRaritySO { get; private set; }
    [SerializeField] private GameObject CharacterPrefab;

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

    public ItemRaritySO GetRaritySO()
    {
        return CharacterRaritySO;
    }

    public IItem GetIItem()
    {
        return this;
    }

    public DamageableCharacters CreateCharacter(Transform transform)
    {
        DamageableCharacters prefab = CharacterPrefab.GetComponent<DamageableCharacters>();

        if (prefab == null)
        {
            Debug.Log("CharacterPrefab does not have the component of DamageableCharacters");
            return null;
        }

        DamageableCharacters DamageableCharacters = Instantiate(CharacterPrefab, transform).GetComponent<DamageableCharacters>();
        DamageableCharacters.CreateCharacterStateMachine();

        return DamageableCharacters;
    }
}
