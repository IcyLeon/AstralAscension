using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance { get; private set; }
    [SerializeField] private CharacterDataSO[] CharacterDataSO;

    private void Awake()
    {
        instance = this;
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
