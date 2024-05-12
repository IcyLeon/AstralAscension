using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ElementalReactionSO", menuName = "ScriptableObjects/ElementManager/ElementalReactionSO")]
public class ElementalReactionSO : ScriptableObject
{
    [field: SerializeField] public string DisplayElementalReactionText { get; private set; }
    [field: SerializeField] public ElementalReactionEnums ElementalReaction { get; private set; }
    [field: SerializeField] public ElementsSO[] ElementsMixture { get; private set; }
    [field: SerializeField] public ElementsSO ElementalDamageOverTimeSO { get; private set; }
    [field: SerializeField] public Color32 ColorText { get; private set; }
    [Range(1f, 5f)]
    [SerializeField] private float ElementalReactionBonus = 1f;

    public float GetElementalReactionBonus()
    {
        return ElementalReactionBonus;
    }
}
