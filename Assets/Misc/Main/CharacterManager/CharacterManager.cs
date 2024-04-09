using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance { get; private set; }
    [Range(0f, 1f)]
    [SerializeField] private float ProbabilityPlayVO;
    [SerializeField] private CharacterDataSO[] CharacterDataSO;

    private void Awake()
    {
        instance = this;
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
