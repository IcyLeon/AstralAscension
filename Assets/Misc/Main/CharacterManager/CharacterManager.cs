using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance { get; private set; }
    [Range(0f, 1f)]
    [SerializeField] private float ProbabilityPlayVO;

    private Dictionary<PlayerCharactersSO, SkinEquipment> playableCharactersSOs = new();

    public CharacterStorage characterStorage { get; private set; }

    public static event Action<CharacterStorage> OnCharacterStorageChanged;


    private void SetCharacterStorage(CharacterStorage CharacterStorage)
    {
        characterStorage = CharacterStorage;
        OnCharacterStorageChanged?.Invoke(characterStorage);
    }

    private void Awake()
    {
        instance = this;
        SetCharacterStorage(new CharacterStorage());
        LoadCharactersSO();
    }

    public CharacterDataStat GetCharacterDataStat(PlayerCharactersSO PlayerCharactersSO)
    {
        return characterStorage.GetCharacterDataStat(PlayerCharactersSO);
    }

    private void LoadCharactersSO()
    {
        PlayerCharactersSO[] playableCharactersSOList = Resources.LoadAll<PlayerCharactersSO>("Characters");

        foreach (var CharactersSO in playableCharactersSOList)
        {
            playableCharactersSOs.Add(CharactersSO, new SkinEquipment(CharactersSO));
            characterStorage.AddCharacterData(CharactersSO, CharactersSO.CreateCharacterDataStat());
        }
    }


    private void Update()
    {
        if (characterStorage != null)
            characterStorage.Update();
    }

    public static bool isInProbabilityRange(float a)
    {
        float randomValue = Random.value;
        float probability = 1.0f - a;
        return randomValue > probability;
    }

    public float GetProbabilityPlayVO()
    {
        return ProbabilityPlayVO;
    }

}
