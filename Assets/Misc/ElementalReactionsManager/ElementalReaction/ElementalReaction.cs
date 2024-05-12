using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static ElementalReactionsManager;

public abstract class ElementalReaction
{
    public IDamageable target { get; private set; }
    public ElementalReactionSO elementalReactionSO { get; protected set; }

    public delegate void OnEREvent(ElementalReaction ER);
    public event OnEREvent OnERDestroy;

    protected ElementsInfo ElementsInfo;

    protected abstract float CalculateERDamage(float BaseDamageAmount, IAttacker source);

    protected float GetReactionMultiplier(float EMBonus)
    {
        return 1f + EMBonus + elementalReactionSO.GetElementalReactionBonus();
    }

    public ElementalReaction(ElementalReactionSO e, ElementsInfo ElementsInfo, IDamageable target)
    {
        elementalReactionSO = e;
        this.target = target;
        this.ElementsInfo = ElementsInfo;
        float DamageAmount = CalculateERDamage(ElementsInfo.DamageAmount, ElementsInfo.source);

        CallElementalReactionDamageInvoke(target, new ElementalReactionInfo
        {
            DamageText = new()
            {
                { DamageAmount.ToString() },
                { elementalReactionSO.DisplayElementalReactionText.ToString() },
            },
            DamageAmount = DamageAmount,
            WorldPosition = target.GetCenterBound(),
            elementalReactionSO = elementalReactionSO,
        });

        RemoveElements();
        Reset();
    }

    public virtual void Reset()
    {

    }
    private void RemoveElements()
    {
        if (target == null)
            return;

        ElementsSO[] elementsSOs = elementalReactionSO.ElementsMixture;

        for (int i = 0; i < elementsSOs.Length; i++)
        {
            target.GetInflictElementLists().Remove(elementsSOs[i]);
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
