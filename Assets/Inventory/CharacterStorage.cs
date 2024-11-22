using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class CharacterStorage
{
    public Dictionary<CharactersSO, PlayableCharacterDataStat> playableCharacterStatList { get; }
    public PartySetupManager PartySetupManager { get; }
    public delegate void OnCharacterStatChanged(CharactersSO CharactersSO);
    public event OnCharacterStatChanged OnCharacterAdd, OnCharacterRemove;

    public void AddCharacterData(CharacterDataStat c)
    {
        PlayableCharacterDataStat playableCharacterDataStat = c as PlayableCharacterDataStat;

        if (playableCharacterDataStat == null || HasObtainedCharacter(c.damageableEntitySO) != null)
            return;

        playableCharacterStatList.Add(playableCharacterDataStat.playerCharactersSO, playableCharacterDataStat);
        OnCharacterAdd?.Invoke(c.damageableEntitySO);
    }

    public PlayableCharacterDataStat GetPlayableCharacterDataStat(CharactersSO CharactersSO)
    {
        if (!playableCharacterStatList.TryGetValue(CharactersSO, out PlayableCharacterDataStat playableCharacterDataStat))
            return null;

        return playableCharacterDataStat;
    }

    public void RemoveCharacterData(CharactersSO c)
    {
        if (c == null)
            return;

        CharacterDataStat CharacterDataStat = HasObtainedCharacter(c);
        if (CharacterDataStat != null)
        {
            CharacterDataStat.OnDestroy();
            playableCharacterStatList.Remove(c);
            OnCharacterRemove?.Invoke(c);
        }
    }

    public CharacterDataStat HasObtainedCharacter(CharactersSO c)
    {
        if (playableCharacterStatList != null && playableCharacterStatList.TryGetValue(c, out PlayableCharacterDataStat pc))
        {
            return pc;
        }

        return null;
    }

    public void Update()
    {
        for (int i = 0; i < playableCharacterStatList.Count; i++)
        {
            CharacterDataStat cd = playableCharacterStatList.ElementAt(i).Value;
            cd.Update();
        }
    }

    public void OnDestroy()
    {
        PartySetupManager.OnDestroy();
    }

    public CharacterStorage()
    {
        playableCharacterStatList = new();
        PartySetupManager = new(this);
    }
}
