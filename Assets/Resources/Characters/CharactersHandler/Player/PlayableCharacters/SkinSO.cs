using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinSO", menuName = "ScriptableObjects/PlayerCharactersManager/SkinSO")]
public class SkinSO : ScriptableObject, IData
{
    [SerializeField] public string Name;
    [SerializeField] private Sprite Icon;
    [SerializeField] public ItemRaritySO RaritySO;
    [TextArea]
    [SerializeField] public string Description;

    [SerializeField] private Sprite Wish;
    [field: SerializeField] public int Cost { get; private set; }
    [SerializeField] private GameObject ModelPrefab;
    [SerializeField] private GameObject GameplayPrefab;

    public GameObject LoadSkin(Transform Parent)
    {
        GameObject skin = Object.Instantiate(ModelPrefab, Parent);
        return skin;
    }

    public GameObject ApplyGameplaySkin(Transform Parent)
    {
        GameObject skin = Object.Instantiate(GameplayPrefab, Parent);
        return skin;
    }

    public string GetName()
    {
        return Name;
    }

    public ItemTypeSO GetTypeSO()
    {
        return null;
    }

    public Sprite GetIcon()
    {
        return Icon;
    }

    public string GetDescription()
    {
        return Description;
    }

    public ItemRaritySO GetRaritySO()
    {
        return RaritySO;
    }
}
