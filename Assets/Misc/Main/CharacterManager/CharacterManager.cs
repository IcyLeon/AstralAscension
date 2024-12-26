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

    private Dictionary<CharactersSO, CharacterDataStat> allplayableCharactersDic;

    public CharacterStorage characterStorage { get; private set; }

    public delegate void OnCharacterStorageChanged(CharacterStorage CharacterStorage);
    public static event OnCharacterStorageChanged OnCharacterStorageOld, OnCharacterStorageNew;


    private void SetCharacterStorage(CharacterStorage CharacterStorage)
    {
        if (characterStorage != null)
            OnCharacterStorageOld?.Invoke(characterStorage);

        characterStorage = CharacterStorage;
        OnCharacterStorageNew?.Invoke(characterStorage);
    }

    private void Awake()
    {
        instance = this;

        SetCharacterStorage(new CharacterStorage());
        LoadCharactersSO();
        TestCharacters();
    }

    public CharacterDataStat GetCharacterDataStat(CharactersSO CharactersSO)
    {
        if (!allplayableCharactersDic.TryGetValue(CharactersSO, out CharacterDataStat characterDataStat))
            return null;

        return characterDataStat;
    }

    private void LoadCharactersSO()
    {
        allplayableCharactersDic = new();
        PlayerCharactersSO[] playableCharactersSOList = Resources.LoadAll<PlayerCharactersSO>("Characters");

        foreach (var pc in playableCharactersSOList)
        {
            allplayableCharactersDic.Add(pc, pc.CreateCharacterDataStat());
        }
    }

    private void TestCharacters()
    {
        foreach (var c in allplayableCharactersDic)
        {
            characterStorage.AddCharacterData(c.Key);
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

    public static bool ContainsParam(Animator _Anim, string _ParamName)
    {
        foreach (AnimatorControllerParameter param in _Anim.parameters)
        {
            if (param.name == _ParamName)
                return true;
        }
        return false;
    }

}
