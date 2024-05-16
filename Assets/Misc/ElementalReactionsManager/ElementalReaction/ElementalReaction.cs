using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static DamageManager;
using static ElementalReactionsManager;

public abstract class ElementalReaction
{
    public IDamageable target { get; private set; }
    public ElementalReactionSO elementalReactionSO { get; protected set; }

    public delegate void OnEREvent(ElementalReaction ER);
    public event OnEREvent OnERDestroy;

    protected ElementDamageInfoEvent ElementDamageInfoEvent;

    protected abstract float CalculateERDamage(float BaseDamageAmount, IAttacker source);

    protected float GetReactionMultiplier(float EMBonus)
    {
        return 1f + EMBonus + elementalReactionSO.GetElementalReactionBonus();
    }

    public ElementalReaction(ElementalReactionSO e, ElementDamageInfoEvent ElementDamageInfoEvent, IDamageable target)
    {
        elementalReactionSO = e;
        this.target = target;
        this.ElementDamageInfoEvent = ElementDamageInfoEvent;

        CallOnDamageTextInvoke(target, new DamageInfo
        {
            DamageText = e.DisplayElementalReactionText,
            ElementsInfoSO = e,
            WorldPosition = target.GetCenterBound()
        });

        SpawnDamageReaction();
        RemoveElements();
    }

    protected virtual void SpawnDamageReaction()
    {
        float DamageAmount = CalculateERDamage(ElementDamageInfoEvent.damageAmount, ElementDamageInfoEvent.source);

        CallElementalReactionDamageInvoke(target, new ElementalReactionDamageInfoEvent
        {
            damageAmount = DamageAmount,
            elementalReactionSO = elementalReactionSO,
        });

    }

    private void RemoveElements()
    {
        if (target == null)
            return;

        ElementsSO[] elementsSOs = elementalReactionSO.ElementsMixture;

        for (int i = 0; i < elementsSOs.Length; i++)
        {
            Elements e = GetElements(target, elementsSOs[i]);
            if (e != null) {
                target.RemoveElement(e.elementsSO, e);
            }
        }
    }

    public virtual void OnDestroy()
    {
        OnERDestroy?.Invoke(this);
    }

    public void DestroyEvents()
    {
        OnERDestroy = null;
    }


    protected virtual bool TimeOut()
    {
        return target == null || target.IsDead();
    }

    public virtual void Update()
    {
        if (TimeOut())
        {
            OnDestroy();
            return;
        }

    }

}
