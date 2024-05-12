using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ElementalReactionsManager;

public abstract class ElementalReactionFactory
{
    public abstract ElementalReaction CreateER(ElementalReactionSO ERSO, ElementsInfo ElementsInfo, IDamageable target);
}

public class FrozenFactory : ElementalReactionFactory
{
    public override ElementalReaction CreateER(ElementalReactionSO ERSO, ElementsInfo ElementsInfo, IDamageable target)
    {
        return new Frozen(ERSO, ElementsInfo, target);
    }
}

public class OverloadFactory : ElementalReactionFactory
{
    public override ElementalReaction CreateER(ElementalReactionSO ERSO, ElementsInfo ElementsInfo, IDamageable target)
    {
        return new Overloaded(ERSO, ElementsInfo, target);
    }
}

public class ElectricChargedFactory : ElementalReactionFactory
{
    public override ElementalReaction CreateER(ElementalReactionSO ERSO, ElementsInfo ElementsInfo, IDamageable target)
    {
        return new ElectroCharged(ERSO, ElementsInfo, target);
    }
}
