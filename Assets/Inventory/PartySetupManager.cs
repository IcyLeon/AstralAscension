using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyEvents : EventArgs
{
    public int PartyIndex;
    public int PartyCharacterLocation;
    public CharactersSO CharactersSO;
}

public class PartySetupManager
{
    public const int MAX_EQUIP_CHARACTERS = 4;
    public const int MAX_PARTY = 4;

    private Dictionary<int, List<CharactersSO>> PartySetupList;

    private int currentPartyIndex;

    public CharacterStorage characterStorage { get; private set; }

    public event EventHandler<PartyEvents> OnPartyAdd, OnPartyRemove;
    public event Action OnCurrentPartyChanged;

    public PartySetupManager(CharacterStorage CharacterStorage)
    {
        characterStorage = CharacterStorage;

        characterStorage.OnCharacterAdd += CharacterStorage_OnCharacterAdd;
        characterStorage.OnCharacterRemove += CharacterStorage_OnCharacterRemove;

        PartySetupList = new();
        // init all party as null
        for (int i = 0; i < MAX_PARTY; i++)
        {
            PartySetupList.Add(i, new List<CharactersSO>());

            for (int j = 0; j < MAX_EQUIP_CHARACTERS; j++) {
                PartySetupList[i].Add(null);
            }
        }

        SetCurrentParty(0);
    }

    public PlayableCharacterDataStat GetPlayableCharacterDataStat(CharactersSO CharactersSO)
    {
        return characterStorage.GetPlayableCharacterDataStat(CharactersSO);
    }

    public void OnDestroy()
    {
        characterStorage.OnCharacterAdd -= CharacterStorage_OnCharacterAdd;
        characterStorage.OnCharacterRemove -= CharacterStorage_OnCharacterRemove;
    }

    private void CharacterStorage_OnCharacterRemove(CharacterDataStat c)
    {
        RemoveAllMembers(c.damageableEntitySO);
    }

    private void CharacterStorage_OnCharacterAdd(CharacterDataStat c)
    {
    }

    public void SetCurrentParty(int index)
    {
        if (index < 0 || index >= PartySetupList.Count)
            return;

        currentPartyIndex = index;

        OnCurrentPartyChanged?.Invoke();
    }

    public List<CharactersSO> GetCurrentPartyLayout()
    {
        return PartySetupList[currentPartyIndex];
    }

    public List<CharactersSO> GetCurrentPartyMembers()
    {
        List<CharactersSO> PartyLayout = new(GetCurrentPartyLayout());

        for(int i = PartyLayout.Count - 1; i >= 0; i--)
        {
            if (PartyLayout[i] == null)
            {
                PartyLayout.RemoveAt(i);
            }
        }

        return PartyLayout;
    }

    public void AddMember(CharactersSO charactersSO, int partySetupIndex, int PartyLocation)
    {
        if (charactersSO == null || characterStorage.HasObtainedCharacter(charactersSO) == null)
            return;

        CharactersSO CharacterInSlot = PartySetupList[partySetupIndex][PartyLocation];

        if (CharacterAlreadyExist(charactersSO, partySetupIndex))
        {
            Debug.Log(charactersSO.GetName() + " is already existed!");
            return;
        }

        if (CharacterInSlot != null)
        {
            Debug.Log(CharacterInSlot.GetName() + " is currently taking this slot!");
            return;
        }

        PartySetupList[partySetupIndex][PartyLocation] = charactersSO;

        OnPartyAdd?.Invoke(this, new PartyEvents
        {
            CharactersSO = charactersSO,
            PartyIndex = partySetupIndex,
            PartyCharacterLocation = PartyLocation
        });

        // update the current party if there is any change
        if (partySetupIndex == currentPartyIndex)
        {
            SetCurrentParty(currentPartyIndex);
        }
    }

    public void AddMember(CharactersSO charactersSO, int partySetupIndex)
    {
        if (charactersSO == null || characterStorage.HasObtainedCharacter(charactersSO) == null)
            return;

        int emptySlot = GetEmptySlot(partySetupIndex);

        if (emptySlot == -1)
            return;

        AddMember(charactersSO, partySetupIndex, emptySlot);
    }

    public void RemoveMember(int partySetupIndex, int partyLocation)
    {
        CharactersSO charactersSO = PartySetupList[partySetupIndex][partyLocation];

        PartySetupList[partySetupIndex][partyLocation] = null;

        OnPartyRemove?.Invoke(this, new PartyEvents
        {
            CharactersSO = charactersSO,
            PartyCharacterLocation = partyLocation,
            PartyIndex = partySetupIndex
        });


        // update the current party if there is any change
        if (partySetupIndex == currentPartyIndex)
        {
            SetCurrentParty(currentPartyIndex);
        }
    }

    public void RemoveMember(CharactersSO charactersSO, int partySetupIndex)
    {
        if (partySetupIndex < 0 || partySetupIndex >= PartySetupList.Count)
            return;

        for (int j = 0; j < PartySetupList[partySetupIndex].Count; j++)
        {
            CharactersSO c = PartySetupList[partySetupIndex][j];

            if (c != null && c == charactersSO)
            {
                RemoveMember(partySetupIndex, j);
            }
        }
    }

    public void RemoveAllMembers(CharactersSO charactersSO)
    {
        List<int>[] CharacterInParties = FindCharacterInParties(charactersSO);

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

        if (partySetupIndex < 0 || partySetupIndex >= PartySetupList.Count)
            return slot;

        List<CharactersSO> PartyMemberList = PartySetupList[partySetupIndex];

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

    private List<int>[] FindCharacterInParties(CharactersSO CharactersSO)
    {
        List<int>[] PartyList = new List<int>[PartySetupList.Count];

        if (CharactersSO == null)
            return PartyList;


        for (int i = 0; i < PartySetupList.Count; i++)
        {
            PartyList[i] = new List<int>();

            for (int j = 0; j < PartySetupList[i].Count; j++)
            {
                if (PartySetupList[i][j] != null &&
                    PartySetupList[i][j] == CharactersSO)
                {
                    PartyList[i].Add(j);
                }
            }
        }

        return PartyList;
    }

    private bool CharacterAlreadyExist(CharactersSO charactersSO, int partySetupIndex)
    {
        if (charactersSO == null || partySetupIndex < 0 || partySetupIndex >= PartySetupList.Count)
            return false;

        foreach (var PartyMember in PartySetupList[partySetupIndex])
        {
            if (PartyMember != null && PartyMember == charactersSO)
                return true;
        }

        return false;
    }
}
