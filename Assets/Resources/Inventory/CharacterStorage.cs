using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class CharacterStorage
{
    public Dictionary<CharactersSO, CharacterDataStat> characterStatList { get; private set; }
    public delegate void OnCharacterStatChanged(CharactersSO CharactersSO);
    public event OnCharacterStatChanged OnCharacterAdd, OnCharacterRemove;

    public void AddCharacterData(CharactersSO CharactersSO)
    {
        if (CharactersSO == null || HasObtainedCharacter(CharactersSO) != null)
            return;

        characterStatList.Add(CharactersSO, CharacterManager.instance.GetCharacterDataStat(CharactersSO));
        OnCharacterAdd?.Invoke(CharactersSO);
    }

    public CharacterDataStat GetCharacterDataStat(CharactersSO CharactersSO)
    {
        if (CharactersSO == null || !characterStatList.TryGetValue(CharactersSO, out CharacterDataStat CharacterDataStat))
            return null;

        return CharacterDataStat;
    }

    public void RemoveCharacterData(CharactersSO c)
    {
        if (c == null)
            return;

        CharacterDataStat CharacterDataStat = HasObtainedCharacter(c);
        if (CharacterDataStat != null)
        {
            CharacterDataStat.OnDestroy();
            characterStatList.Remove(c);
            OnCharacterRemove?.Invoke(c);
        }
    }

    public CharacterDataStat HasObtainedCharacter(CharactersSO c)
    {
        if (c == null || !characterStatList.TryGetValue(c, out CharacterDataStat pc))
            return null;

        return pc;
    }

    public void Update()
    {
        for (int i = 0; i < characterStatList.Count; i++)
        {
            CharacterDataStat cd = characterStatList.ElementAt(i).Value;
            cd.Update();
        }
    }

    public CharacterStorage()
    {
        characterStatList = new();
    }
}
