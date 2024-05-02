using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ElementalReactionSO", menuName = "ScriptableObjects/ElementManager/ElementalReactionSO")]
public class ElementalReactionSO : ScriptableObject
{
    [field: SerializeField] public string DisplayElementalReactionText { get; private set; }
    [field: SerializeField] public ElementalReactionEnums ElementalReaction { get; private set; }
    [field: SerializeField] public Color32 Color { get; private set; }
    [Range(1f, 2f)]
    [SerializeField] private float DamageMultiplier = 1f;

    public float GetDamageMultiplier()
    {
        return DamageMultiplier;
    }
}
