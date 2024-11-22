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

    private void OnDestroy()
    {
        if (characterStorage != null)
            characterStorage.OnDestroy();    
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
