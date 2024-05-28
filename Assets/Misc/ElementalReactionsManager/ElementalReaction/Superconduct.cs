using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ElementalReactionsManager;

public class Superconduct : ElementalReaction
{
    public Superconduct(ElementalReactionSO e, ElementDamageInfoEvent ElementDamageInfoEvent, IDamageable target) : base(e, ElementDamageInfoEvent, target)
    {
        OnDestroy();
    }

    protected override float CalculateERDamage(float DamageAmount, IAttacker source)
    {
        if (source == null)
            return 0f;

        float EMBonus = 2.78f * (source.GetEM() / (source.GetEM() + 1400)) * 0.01f;

        return DamageAmount * GetReactionMultiplier(EMBonus);
    }
}
