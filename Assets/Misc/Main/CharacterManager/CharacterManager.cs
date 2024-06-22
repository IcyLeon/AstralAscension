using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterManager : MonoBehaviour
{
    public const int MAX_EQUIP_CHARACTERS = 4;
    public static CharacterManager instance { get; private set; }
    [Range(0f, 1f)]
    [SerializeField] private float ProbabilityPlayVO;
    [SerializeField] private CharacterDataSO[] CharacterDataSO;

    private CharacterStorage characterStorage;

    public delegate void OnCharacterStorageChanged(CharacterStorage CharacterStorage);
    public static event OnCharacterStorageChanged OnCharacterStorageOld, OnCharacterStorageNew;

    public GameObject GetCharacterPrefab(CharactersSO charactersSO)
    {
        foreach(var characterDataSO in CharacterDataSO)
        {
            if (characterDataSO.charactersSO == charactersSO)
                return characterDataSO.CharacterPrefab;
        }
        return null;
    }

    private void SetCharacterStorage(CharacterStorage CharacterStorage)
    {
        if (characterStorage != null)
        {
            OnCharacterStorageOld?.Invoke(characterStorage);
        }
        characterStorage = CharacterStorage;
        OnCharacterStorageNew?.Invoke(characterStorage);
    }

    //public void SpawnSavedCharacters(Transform parentTransform)
    //{
    //    if (characterStorage == null)
    //        return;

    //    foreach (var characterDataStat in characterStorage.playableCharacterStatList)
    //    {
    //        GameObject characterPrefab = GetCharacterPrefab(characterDataStat.Key);
    //        if (characterPrefab != null)
    //        {
    //            IDamageable IDamageable = Instantiate(characterPrefab, parentTransform).GetComponent<IDamageable>();
    //            IDamageable.SetCharacterDataStat(characterStorage.playableCharacterStatList[characterDataStat.Key]);
    //        }
    //    }
    //}

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetCharacterStorage(new CharacterStorage());
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
