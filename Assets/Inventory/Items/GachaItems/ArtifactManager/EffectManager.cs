using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager
{
    private List<BuffEffect> buffEffectList;
    public event EventHandler<BuffEvent> OnBuffRemove;

    public EffectManager()
    {
        buffEffectList = new();
    }

    public void RemoveEffect(BuffEffect buffEffect)
    {
        if (!buffEffectList.Remove(buffEffect))
            return;

        Debug.Log("Remove " + buffEffect.GetType());
        OnBuffRemove?.Invoke(this, new BuffEvent { BuffEffect = buffEffect });
    }


    public void AddEffect(BuffEffect buffEffect)
    {
        if (GetBuffTypeAlreadyExist(buffEffect) != null)
            return;

        buffEffectList.Add(buffEffect);
        Debug.Log("Add " + buffEffect.GetType());
        SubscribeBuffEvents(buffEffect);
    }
    public BuffEffect GetBuffTypeAlreadyExist(BuffEffect BuffEffect)
    {
        foreach(var existbuffEffect in buffEffectList)
        {
            if (existbuffEffect.GetType() == BuffEffect.GetType())
            {
                return existbuffEffect;
            }
        }
        return null;
    }

    private void BuffEffect_OnBuffRemove(object sender, BuffEvent e)
    {
        RemoveEffect(e.BuffEffect);

        UnsubscribeBuffEvents(e.BuffEffect);
    }

    public void Update()
    {
        for(int i = 0; i < buffEffectList.Count; i++)
        {
            buffEffectList[i].Update();
        }
    }

    public void OnDestroy()
    {
        for (int i = buffEffectList.Count - 1; i > 0; i--)
        {
            UnsubscribeBuffEvents(buffEffectList[i]);
        }
    }

    private void UnsubscribeBuffEvents(BuffEffect buffEffect)
    {
        buffEffect.OnBuffRemove -= BuffEffect_OnBuffRemove;
    }

    private void SubscribeBuffEvents(BuffEffect buffEffect)
    {
        buffEffect.OnBuffRemove += BuffEffect_OnBuffRemove;
    }
}
