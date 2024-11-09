using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementDamageInfoEvent : EventArgs
{
    public ElementsInfoSO elementsInfoSO;
    public float damageAmount;
    public Vector3 hitPosition;
    public IAttacker source;
    public IDamageable target;
}

public class ElementalReactionMiscEvents
{
    public event Action<ElementDamageInfoEvent> OnElementDamageEvent;
    public event Action<ElementDamageInfoEvent> OnAddElementEvent;

    public void TakeDamage(ElementDamageInfoEvent e)
    {
        OnElementDamageEvent?.Invoke(e);
    }

    public void AddElement(ElementDamageInfoEvent e)
    {
        OnAddElementEvent?.Invoke(e);
    }
}
