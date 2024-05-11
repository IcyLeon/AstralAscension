using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static ElementalReactionsManager;

public abstract class ElementalReaction
{
    public IDamageable target { get; private set; }
    public ElementalReactionSO elementalReactionSO { get; protected set; }

    public delegate void OnER(ElementalReaction ER);
    public OnER OnERDestroy;

    protected ElementsInfo ElementsInfo;

    protected abstract float CalculateERDamage(float BaseDamageAmount, IElement source);

    protected float GetReactionMultiplier(float EMBonus)
    {
        return 1f + EMBonus + elementalReactionSO.GetElementalReactionBonus();
    }

    public ElementalReaction(ElementalReactionSO e)
    {
        elementalReactionSO = e;
    }

    public virtual void Init(ElementsInfo ElementsInfo, IDamageable target)
    {
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
            WorldPosition = target.GetMiddleBound(),
            elementalReactionSO = elementalReactionSO,
        });
    }

    private void RemoveElements()
    {
        if (target == null)
            return;

        ElementsSO[] elementsSOs = instance.GetElementsSOList(elementalReactionSO);

        for (int i = 0; i < elementsSOs.Length; i++)
        {
            target.GetSelfInflictElementLists().Remove(elementsSOs[i]);
        }

    }
    public virtual void OnDestroy()
    {
        RemoveElements();
        OnERDestroy?.Invoke(this);
    }

    public virtual void Update()
    {

    }

}
