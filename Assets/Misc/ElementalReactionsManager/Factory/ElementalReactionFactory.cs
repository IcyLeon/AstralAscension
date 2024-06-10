using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ElementalReactionsManager;

public enum ElementalReactionEnums
{
    NONE,
    OVERLOAD,
    MELT,
    BURN,
    FROZEN,
    ELECTRO_CHARGED,
    VAPORIZED,
    SUPERCONDUCT,
}

public static class ElementalReactionFactoryManager
{
    private static Dictionary<ElementalReactionEnums, System.Func<ElementalReactionFactory>> ER_Dict = new()
    {
        { ElementalReactionEnums.OVERLOAD, () => new OverloadFactory() },
        { ElementalReactionEnums.FROZEN, () => new FrozenFactory() },
        { ElementalReactionEnums.SUPERCONDUCT, () => new SuperconductFactory() },
        { ElementalReactionEnums.ELECTRO_CHARGED, () => new ElectricChargedFactory() }
    };

    public static ElementalReactionFactory CreateElementalReactionFactory(ElementalReactionEnums e)
    {
        if (!ER_Dict.ContainsKey(e))
            return null;

        return ER_Dict[e]();
    }
}

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

public class SuperconductFactory : ElementalReactionFactory
{
    public override ElementalReaction CreateER(ElementalReactionSO ERSO, ElementDamageInfoEvent ElementDamageInfoEvent, IDamageable target)
    {
        return new Superconduct(ERSO, ElementDamageInfoEvent, target);
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
