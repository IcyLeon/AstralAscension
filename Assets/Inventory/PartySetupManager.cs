using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartySetupManager
{
    public class PartyEvents : EventArgs
    {
        public int PartyIndex;
        public int PartyCharacterLocation;
        public CharacterDataStat CharacterDataStat;
    }

    public const int MAX_EQUIP_CHARACTERS = 4;
    public const int MAX_PARTY = 4;

    private List<CharacterDataStat>[] PartySetupList;

    private int currentPartyIndex;

    private CharacterStorage characterStorage;

    public event EventHandler<PartyEvents> OnPartyAdd, OnPartyRemove;
    public event EventHandler OnCurrentPartyChanged;

    public PartySetupManager(CharacterStorage CharacterStorage)
    {
        characterStorage = CharacterStorage;

        characterStorage.OnCharacterAdd += CharacterStorage_OnCharacterAdd;
        characterStorage.OnCharacterRemove += CharacterStorage_OnCharacterRemove;

        PartySetupList = new List<CharacterDataStat>[MAX_PARTY];

        // init all party as null
        for (int i = 0; i < MAX_PARTY; i++)
        {
            PartySetupList[i] = new List<CharacterDataStat>();

            for (int j = 0; j < MAX_EQUIP_CHARACTERS; j++) {
                PartySetupList[i].Add(null);
            }
        }

        SetCurrentParty(0);
    }

    public void OnDestroy()
    {
        characterStorage.OnCharacterAdd -= CharacterStorage_OnCharacterAdd;
        characterStorage.OnCharacterRemove -= CharacterStorage_OnCharacterRemove;
    }

    private void CharacterStorage_OnCharacterRemove(CharacterDataStat c)
    {
        RemoveAllMembers(c);
    }

    private void CharacterStorage_OnCharacterAdd(CharacterDataStat c)
    {
    }

    public void SetCurrentParty(int index)
    {
        if (index < 0 || index >= PartySetupList.Length)
            return;

        currentPartyIndex = index;

        OnCurrentPartyChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<CharacterDataStat> GetCurrentPartyLayout()
    {
        return PartySetupList[currentPartyIndex];
    }

    public List<CharacterDataStat> GetCurrentPartyMembers()
    {
        List<CharacterDataStat> PartyLayout = new(GetCurrentPartyLayout());

        for(int i = 0; i < PartyLayout.Count; i++)
        {
            if (PartyLayout[i] == null)
            {
                PartyLayout.RemoveAt(i);
            }
        }

        return PartyLayout;
    }

    public void AddMember(CharacterDataStat characterDataStat, int partySetupIndex, int PartyLocation)
    {
        if (characterDataStat == null && characterStorage.HasObtainedCharacter(characterDataStat.damageableEntitySO) == null)
            return;

        CharacterDataStat CharacterInSlot = PartySetupList[partySetupIndex][PartyLocation];

        if (CharacterAlreadyExist(characterDataStat, partySetupIndex))
        {
            Debug.Log(characterDataStat.damageableEntitySO.characterName + " is already existed!");
            return;
        }

        if (CharacterInSlot != null)
        {
            Debug.Log(CharacterInSlot.damageableEntitySO.characterName + " is currently taking this slot!");
            return;
        }

        PartySetupList[partySetupIndex][PartyLocation] = characterDataStat;

        OnPartyAdd?.Invoke(this, new PartyEvents
        {
            CharacterDataStat = characterDataStat,
            PartyIndex = partySetupIndex,
            PartyCharacterLocation = PartyLocation
        });

        // update the current party if there is any change
        if (partySetupIndex == currentPartyIndex)
        {
            SetCurrentParty(currentPartyIndex);
        }
    }

    public void AddMember(CharacterDataStat characterDataStat, int partySetupIndex)
    {
        if (characterDataStat == null && characterStorage.HasObtainedCharacter(characterDataStat.damageableEntitySO) == null)
            return;

        int emptySlot = GetEmptySlot(partySetupIndex);

        if (emptySlot == -1)
            return;

        AddMember(characterDataStat, partySetupIndex, emptySlot);
    }

    public void RemoveMember(int partySetupIndex, int partyLocation)
    {
        OnPartyRemove?.Invoke(this, new PartyEvents
        {
            CharacterDataStat = PartySetupList[partySetupIndex][partyLocation],
            PartyCharacterLocation = partyLocation,
            PartyIndex = partySetupIndex
        });

        PartySetupList[partySetupIndex][partyLocation] = null;

        // update the current party if there is any change
        if (partySetupIndex == currentPartyIndex)
        {
            SetCurrentParty(currentPartyIndex);
        }
    }

    public void RemoveMember(CharacterDataStat characterDataStat, int partySetupIndex)
    {
        if (partySetupIndex < 0 || partySetupIndex >= PartySetupList.Length)
            return;

        for (int j = 0; j < PartySetupList[partySetupIndex].Count; j++)
        {
            CharacterDataStat c = PartySetupList[partySetupIndex][j];

            if (c != null && c.damageableEntitySO == characterDataStat.damageableEntitySO)
            {
                RemoveMember(partySetupIndex, j);
            }
        }
    }

    public void RemoveAllMembers(CharacterDataStat characterDataStat)
    {
        List<int>[] CharacterInParties = FindCharacterInParties(characterDataStat);

        // CharacterInParties[i][j] gets the character location

        for (int i = 0; i < CharacterInParties.Length; i++)
        {
            for (int j = 0; j < CharacterInParties[i].Count; j++)
            {
                RemoveMember(i, CharacterInParties[i][j]);
            }
        }
    }

    private int GetEmptySlot(int partySetupIndex)
    {
        int slot = -1;

        if (partySetupIndex < 0 || partySetupIndex >= PartySetupList.Length)
            return slot;

        List<CharacterDataStat> PartyMemberList = PartySetupList[partySetupIndex];

        for (int i = 0; i < PartyMemberList.Count; i++)
        {
            if (PartyMemberList[i] == null)
            {
                slot = i;
                return slot;
            }
        }

        return slot;
    }

    private List<int>[] FindCharacterInParties(CharacterDataStat CharacterDataStat)
    {
        List<int>[] PartyList = new List<int>[PartySetupList.Length];

        if (CharacterDataStat == null)
            return PartyList;


        for (int i = 0; i < PartySetupList.Length; i++)
        {
            PartyList[i] = new List<int>();

            for (int j = 0; j < PartySetupList[i].Count; j++)
            {
                if (PartySetupList[i][j] != null &&
                    PartySetupList[i][j].damageableEntitySO == CharacterDataStat.damageableEntitySO)
                {
                    PartyList[i].Add(j);
                }
            }
        }

        return PartyList;
    }

    private bool CharacterAlreadyExist(CharacterDataStat CharacterDataStat, int partySetupIndex)
    {
        if (CharacterDataStat == null || partySetupIndex < 0 || partySetupIndex >= PartySetupList.Length)
            return false;

        List<CharacterDataStat> PartyMemberList = PartySetupList[partySetupIndex];

        foreach (var PartyMember in PartyMemberList)
        {
            if (PartyMember != null && PartyMember.damageableEntitySO == CharacterDataStat.damageableEntitySO)
                return true;
        }

        return false;
    }
}
