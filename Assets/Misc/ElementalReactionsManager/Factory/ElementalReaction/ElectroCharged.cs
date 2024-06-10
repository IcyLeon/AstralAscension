using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ElementalReactionsManager;

public class ElectroCharged : ElementalReaction
{
    private const float TickTime = 1f;
    private const float ActiveTimer = 3.5f;
    private float TickElapsed;
    private float activeElasped;

    public ElectroCharged(ElementalReactionSO e, ElementDamageInfoEvent ElementDamageInfoEvent, IDamageable target) : base(e, ElementDamageInfoEvent, target)
    {
        TickElapsed = Time.time;
        activeElasped = Time.time;
    }

    protected override float CalculateERDamage(float DamageAmount, IAttacker source)
    {
        if (source == null)
            return 0f;

        float EMBonus = 2.78f * (source.GetEM() / (source.GetEM() + 1400)) * 0.01f;

        return DamageAmount * GetReactionMultiplier(EMBonus);
    }

    protected override bool TimeOut()
    {
        return base.TimeOut() || Time.time - activeElasped > ActiveTimer;
    }

    public override void Update()
    {
        base.Update();

        if (Time.time - TickElapsed > TickTime)
        {
            target.TakeDamage(ElementDamageInfoEvent.source, elementalReactionSO.ElementalDamageOverTimeSO, 100f);
            TickElapsed = Time.time;
        }
    }
}
