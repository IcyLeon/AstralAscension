using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDataSO", menuName = "ScriptableObjects/CharacterManager/CharacterDataSO")]
public class CharacterDataSO : ScriptableObject
{
    public CharactersSO charactersSO;
    public GameObject CharacterPrefab;
}
