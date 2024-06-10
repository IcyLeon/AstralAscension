using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ElementalReactionsManager;

[CreateAssetMenu(fileName = "ElementalReactionSO", menuName = "ScriptableObjects/ElementManager/ElementalReactionSO")]
public class ElementalReactionSO : ElementsInfoSO
{
    [field: SerializeField] public string DisplayElementalReactionText { get; private set; }
    [field: SerializeField] public ElementalReactionEnums ElementalReaction { get; private set; }
    [field: SerializeField] public ElementsSO[] ElementsMixture { get; private set; }
    [field: SerializeField] public ElementsSO ElementalDamageOverTimeSO { get; private set; }
    [Range(1f, 5f)]
    [SerializeField] private float ElementalReactionBonus = 1f;

    public float GetElementalReactionBonus()
    {
        return ElementalReactionBonus;
    }

    public ElementalReaction CreateElementalReaction(IDamageable target, ElementDamageInfoEvent ElementsInfo)
    {
        ElementalReactionFactory factory = ElementalReactionFactoryManager.CreateElementalReactionFactory(ElementalReaction);

        if (factory == null)
            return null;

        return factory.CreateER(this, ElementsInfo, target);
    }
}
