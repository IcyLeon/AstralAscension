using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ElementalReactionsManager;

public class Overloaded : ElementalReaction
{
    public Overloaded(ElementalReactionSO e, ElementDamageInfoEvent ElementDamageInfoEvent, IDamageable target) : base(e, ElementDamageInfoEvent, target)
    {
        KnockbackEffect(target);
        OnDestroy();
    }

    private Vector3 GetKnockBackForce()
    {
        return Vector3.up * 200f;
    }

    private void KnockbackEffect(IDamageable target)
    {
        IKnockBack knockBackEntity = target as IKnockBack;
        if (knockBackEntity == null)
            return;
        knockBackEntity.KnockBack(GetKnockBackForce());
    }

    protected override float CalculateERDamage(float DamageAmount, IAttacker source)
    {
        if (source == null)
            return 0f;

        float EMBonus = 2.78f * (source.GetEM() / (source.GetEM() + 1400f)) * 0.01f;

        return DamageAmount * GetReactionMultiplier(EMBonus);
    }
}
