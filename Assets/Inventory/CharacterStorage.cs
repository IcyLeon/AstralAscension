using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using static CharacterManager;

public class CharacterEquippedManager
{
    public class CharacterEquipEvents : EventArgs
    {
        public CharacterDataStat CharacterDataStat;
    }

    private CharacterStorage characterStorage;
    private CharacterDataStat[] equippedCharacterStat;

    public event EventHandler<CharacterEquipEvents> OnEquipAdd, OnEquipRemove;
    public event EventHandler OnEquipChanged;

    public void AddEquipPlayableCharacterToList(CharacterDataStat c)
    {
        if (c == null && !characterStorage.HasObtainedCharacter(c))
            return;

        int index = GetUnknownSlot();
        int ExistedCharacterSlot = GetCharacterInEquippedList(c);

        if (ExistedCharacterSlot == -1 && index != -1)
        {
            equippedCharacterStat[index] = c;
            OnEquipAdd?.Invoke(this, new CharacterEquipEvents { CharacterDataStat = c });
            OnEquipChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public CharacterDataStat GetEquipCharacterAt(int index)
    {
        return equippedCharacterStat[index];
    }

    public void RemoveEquipPlayableCharacterToList(int index)
    {
        if (GetEquipCharacterAt(index) == null)
            return;

        OnEquipRemove?.Invoke(this, new CharacterEquipEvents { CharacterDataStat = equippedCharacterStat[index] });
        equippedCharacterStat[index] = null;
        OnEquipChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<CharacterDataStat> GetEquippedCharacterStat()
    {
        List<CharacterDataStat> list = new();
        for (int i = 0; i < equippedCharacterStat.Length; i++)
        {
            CharacterDataStat CharacterDataStat = equippedCharacterStat[i];
            if (CharacterDataStat != null)
                list.Add(CharacterDataStat);
        }
        return list;
    }

    public void Swap(int first, int second)
    {
        if (first < 0 || first >= equippedCharacterStat.Length || second < 0 || second >= equippedCharacterStat.Length)
        {
            return;
        }

        OnEquipRemove?.Invoke(this, new CharacterEquipEvents { CharacterDataStat = equippedCharacterStat[first] });
        OnEquipRemove?.Invoke(this, new CharacterEquipEvents { CharacterDataStat = equippedCharacterStat[second] });

        CharacterDataStat temp = equippedCharacterStat[first];
        equippedCharacterStat[first] = equippedCharacterStat[second];
        equippedCharacterStat[second] = temp;

        OnEquipAdd?.Invoke(this, new CharacterEquipEvents { CharacterDataStat = equippedCharacterStat[first] });
        OnEquipAdd?.Invoke(this, new CharacterEquipEvents { CharacterDataStat = equippedCharacterStat[second] });

        OnEquipChanged?.Invoke(this, EventArgs.Empty);

    }

    private int GetUnknownSlot()
    {
        for (int i = 0; i < equippedCharacterStat.Length; i++)
        {
            if (equippedCharacterStat[i] == null)
            {
                return i;
            }
        }
        return -1;
    }

    private int GetCharacterInEquippedList(CharacterDataStat c)
    {
        for (int i = 0; i < equippedCharacterStat.Length; i++)
        {
            if (equippedCharacterStat[i] != null && equippedCharacterStat[i].damageableEntitySO == c.damageableEntitySO)
                return i;
        }
        return -1;
    }


    public CharacterEquippedManager(CharacterStorage characterStorage)
    {
        equippedCharacterStat = new CharacterDataStat[MAX_EQUIP_CHARACTERS];
        this.characterStorage = characterStorage;
    }
}


public class CharacterStorage
{
    private Dictionary<ItemTypeSO, Artifact> artifactsList; // equipped artifacts character
    private Dictionary<CharactersSO, CharacterDataStat> playableCharacterStatList;

    public CharacterEquippedManager characterEquippedManager { get; }
    public delegate void OnCharacterStatChanged(CharacterDataStat c);
    public event OnCharacterStatChanged OnCharacterAdd, OnCharacterRemove;

    public void AddCharacterData(CharacterDataStat c)
    {
        if (c == null)
            return;

        if (!HasObtainedCharacter(c))
        {
            playableCharacterStatList.Add(c.damageableEntitySO, c);
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

    public void TryAddArtifacts(Artifact artifact)
    {
        if (artifact == null)
            return;

        ItemTypeSO itemTypeSO = artifact.GetItemType();

        if (artifactsList.ContainsKey(itemTypeSO))
            return;

        artifactsList.Add(itemTypeSO, artifact);

    }


    public CharacterStorage()
    {
        playableCharacterStatList = new();
        artifactsList = new();
        characterEquippedManager = new(this);
    }
}
