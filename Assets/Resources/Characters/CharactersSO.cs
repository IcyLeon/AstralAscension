using UnityEngine;

public class CharactersSO : ScriptableObject, IData
{
    [field: SerializeField] public CharacterProfileSO ProfileSO { get; private set; }
    [field: SerializeField] public ItemTypeSO CharacterTypeSO { get; private set; }
    [field: SerializeField] protected GameObject Prefab { get; private set; }

    public string GetDescription()
    {
        return ProfileSO.Description;
    }

    public Sprite GetIcon()
    {
        return ProfileSO.Icon;
    }

    public string GetName()
    {
        return ProfileSO.Name;
    }
    public ItemTypeSO GetTypeSO()
    {
        return CharacterTypeSO;
    }

    public ItemRaritySO GetRaritySO()
    {
        return ProfileSO.RaritySO;
    }
}
