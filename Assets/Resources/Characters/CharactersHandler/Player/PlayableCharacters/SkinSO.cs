using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinSO", menuName = "ScriptableObjects/PlayerCharactersManager/SkinSO")]
public class SkinSO : ScriptableObject
{
    [field: SerializeField] public string SkinName { get; private set; }
    [field: SerializeField] public Sprite SkinImage { get; private set; }
    [field: SerializeField] public int Cost { get; private set; }
}
