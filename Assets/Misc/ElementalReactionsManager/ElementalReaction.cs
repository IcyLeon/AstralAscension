using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementalReaction
{
    protected ElementalReactionSO ElementalReactionSO;

    public void SetElementalReactionSO(ElementalReactionSO e)
    {
        ElementalReactionSO = e;
    }

    public ElementalReaction()
    {
    }

    public virtual void Update()
    {

    }
}
