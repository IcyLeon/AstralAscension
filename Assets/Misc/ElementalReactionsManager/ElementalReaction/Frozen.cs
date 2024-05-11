using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ElementalReactionsManager;

public class Frozen : ElementalReaction
{
    public Frozen(ElementalReactionSO e) : base(e)
    {
    }

    public override void Init(ElementsInfo ElementsInfo, IDamageable target)
    {
        base.Init(ElementsInfo, target);
        OnDestroy();
    }

    protected override float CalculateERDamage(float DamageAmount, IElement source)
    {
        if (source == null)
            return 0f;

        float EMBonus = 2.78f * (source.GetEM() / (source.GetEM() + 1400)) * 0.01f;

        return DamageAmount * GetReactionMultiplier(EMBonus);
    }
}
