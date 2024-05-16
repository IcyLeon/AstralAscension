using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ElementalReactionsManager;

public abstract class ElementalReactionFactory
{
    public abstract ElementalReaction CreateER(ElementalReactionSO ERSO, ElementDamageInfoEvent ElementDamageInfoEvent, IDamageable target);
}

public class FrozenFactory : ElementalReactionFactory
{
    public override ElementalReaction CreateER(ElementalReactionSO ERSO, ElementDamageInfoEvent ElementDamageInfoEvent, IDamageable target)
    {
        return new Frozen(ERSO, ElementDamageInfoEvent, target);
    }
}

public class OverloadFactory : ElementalReactionFactory
{
    public override ElementalReaction CreateER(ElementalReactionSO ERSO, ElementDamageInfoEvent ElementDamageInfoEvent, IDamageable target)
    {
        return new Overloaded(ERSO, ElementDamageInfoEvent, target);
    }
}

public class ElectricChargedFactory : ElementalReactionFactory
{
    public override ElementalReaction CreateER(ElementalReactionSO ERSO, ElementDamageInfoEvent ElementDamageInfoEvent, IDamageable target)
    {
        return new ElectroCharged(ERSO, ElementDamageInfoEvent, target);
    }
}
