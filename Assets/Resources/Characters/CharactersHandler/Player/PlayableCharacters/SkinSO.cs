using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinSO", menuName = "ScriptableObjects/PlayerCharactersManager/SkinSO")]
public class SkinSO : ItemSO, IData
{
    [SerializeField] private Sprite Wish;
    [field: SerializeField] public int Cost { get; private set; }
    [SerializeField] private GameObject ModelPrefab;
    [SerializeField] private GameObject GameplayPrefab;

    public GameObject LoadSkin()
    {
        GameObject skin = Object.Instantiate(ModelPrefab);
        return skin;
    }

    public GameObject ApplyGameplaySkin()
    {
        GameObject skin = Object.Instantiate(GameplayPrefab);
        return skin;
    }
}
