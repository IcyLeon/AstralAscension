using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance { get; private set; }
    [Range(0f, 1f)]
    [SerializeField] private float ProbabilityPlayVO;
    [SerializeField] private CharacterDataSO[] CharacterDataSO;

    private CharacterStorage characterStorage;
    public delegate void OnCharacterStatChanged(CharacterDataStat c);
    public static OnCharacterStatChanged OnCharacterAdd, OnCharacterRemove;
    public static OnCharacterStatChanged OnEquippedCharacter, OnUnEquippedCharacter;

    private GameObject SpawnCharacter(CharactersSO charactersSO)
    {
        foreach(var characterDataSO in CharacterDataSO)
        {
            if (characterDataSO.charactersSO == charactersSO)
                return characterDataSO.CharacterPrefab;
        }
        return null;
    }

    public void AddCharacterData(CharacterDataStat c)
    {
        if (characterStorage == null)
            return;

        PlayerCharactersSO pcSO = c.charactersSO as PlayerCharactersSO;
        if (pcSO == null)
            return;

        if (!characterStorage.playableCharacterStatList.ContainsKey(pcSO))
        {
            characterStorage.playableCharacterStatList.Add(pcSO, c);
            OnCharacterAdd?.Invoke(c);
        }
    }

    public void RemoveCharacterData(CharacterDataStat c)
    {
        if (characterStorage == null)
            return;

        PlayerCharactersSO pcSO = c.charactersSO as PlayerCharactersSO;
        if (pcSO == null)
            return;

        if (characterStorage.playableCharacterStatList.ContainsKey(pcSO))
        {
            characterStorage.playableCharacterStatList.Remove(pcSO);
            OnCharacterRemove?.Invoke(c);
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        characterStorage = new CharacterStorage();
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
