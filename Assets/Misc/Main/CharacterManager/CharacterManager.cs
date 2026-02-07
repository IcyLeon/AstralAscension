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

    private Dictionary<CharactersSO, SkinStorage> playableCharactersSOs = new();

    public CharacterStorage mainCharacterStorage { get; private set; }

    public static event Action<CharacterStorage> OnCharacterStorageChanged;


    private void SetCharacterStorage(CharacterStorage CharacterStorage)
    {
        mainCharacterStorage = CharacterStorage;
        OnCharacterStorageChanged?.Invoke(mainCharacterStorage);
    }

    private void Awake()
    {
        instance = this;
        SetCharacterStorage(new CharacterStorage());
        LoadCharactersSO();
    }

    public SkinStorage GetSkinStorage(CharactersSO CharactersSO)
    {
        if (!playableCharactersSOs.ContainsKey(CharactersSO))
            return null;

        return playableCharactersSOs[CharactersSO];
    }

    public CharacterDataStat GetCharacterDataStat(PlayerCharactersSO PlayerCharactersSO)
    {
        return mainCharacterStorage.GetCharacterDataStat(PlayerCharactersSO);
    }

    private void LoadCharactersSO()
    {
        PlayerCharactersSO[] playableCharactersSOList = Resources.LoadAll<PlayerCharactersSO>("Characters");

        foreach (var CharactersSO in playableCharactersSOList)
        {
            playableCharactersSOs.Add(CharactersSO, new SkinStorage(CharactersSO.GetPlayerCharacterProfileSO()));
            mainCharacterStorage.AddCharacterData(CharactersSO, CharactersSO.CreateCharacterDataStat());
        }
    }


    private void Update()
    {
        if (mainCharacterStorage != null)
            mainCharacterStorage.Update();
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
