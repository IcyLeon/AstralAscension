using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ElementalBurstSO", menuName = "ScriptableObjects/PlayerCharactersManager/ElementalBurstSO")]
public class ElementalBurstSO : SkillSO
{
    [field: SerializeField] public int BurstEnergyCost { get; private set; }
}
