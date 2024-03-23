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

}
