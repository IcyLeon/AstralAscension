using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager
{
    private ArtifactEffectManager artifactEffectManager;

    private List<BuffEffect> buffEffectList;
    public event Action<BuffEvent> OnBuffRemove;

    public EffectManager(CharacterEquipmentManager characterEquipmentManager)
    {
        buffEffectList = new();
        artifactEffectManager = new ArtifactEffectManager(characterEquipmentManager.artifactEquipment, this);
        characterEquipmentManager.SetEffectManager(this);
    }

    public void RemoveEffect(BuffEffect buffEffect)
    {
        if (!buffEffectList.Remove(buffEffect))
            return;

        Debug.Log("Remove " + buffEffect.GetType());

        OnBuffRemove?.Invoke(new BuffEvent { BuffEffect = buffEffect });
    }

    public int GetArtifactBuffCurrentIndex(ArtifactFamilySO artifactFamilySO)
    {
        if (artifactEffectManager == null)
            return -1;

        return artifactEffectManager.GetBuffCurrentIndex(artifactFamilySO);
    }

    public void AddEffect(BuffEffect buffEffect)
    {
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

    private void BuffEffect_OnBuffRemove(BuffEvent e)
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

        artifactEffectManager.OnDestroy();
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
