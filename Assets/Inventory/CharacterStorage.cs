using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class CharacterStorage
{
    public Dictionary<CharactersSO, PlayableCharacterDataStat> playableCharacterStatList { get; }
    public PartySetupManager PartySetupManager { get; }
    public delegate void OnCharacterStatChanged(CharacterDataStat c);
    public event OnCharacterStatChanged OnCharacterAdd, OnCharacterRemove;

    public void AddCharacterData(CharacterDataStat c)
    {
        if (c == null)
            return;

        if (!HasObtainedCharacter(c))
        {
            playableCharacterStatList.Add(c.damageableEntitySO, c as PlayableCharacterDataStat);
            OnCharacterAdd?.Invoke(c);
        }
    }

    public void RemoveCharacterData(CharacterDataStat c)
    {
        if (c == null)
            return;

        if (HasObtainedCharacter(c))
        {
            playableCharacterStatList.Remove(c.damageableEntitySO);
            OnCharacterRemove?.Invoke(c);
        }
    }

    public bool HasObtainedCharacter(CharacterDataStat c)
    {
        if (playableCharacterStatList == null)
            return false;

        return playableCharacterStatList.ContainsKey(c.damageableEntitySO);
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
