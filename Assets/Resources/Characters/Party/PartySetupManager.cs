using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PartySetupManager
{
    private List<PartySetup> PartySetupList;
    public PartySetup currentPartySetup { get; private set; } 
    public event Action OnCurrentPartyChanged;
    public event EventHandler<PartyEvents> OnPartyAdd;
    public event EventHandler<PartyEvents> OnPartyRemove;

    public PartySetupManager(int NoOfParties)
    {
        PartySetupList = new();

        for (int i = 0; i < NoOfParties; i++)
        {
            PartySetup partySetup = new PartySetup();
            partySetup.OnPartyAdd += PartySetup_OnPartyAdd;
            partySetup.OnPartyRemove += PartySetup_OnPartyRemove;
            PartySetupList.Add(partySetup);
        }

        SetCurrentParty(0);
    }

    private void PartySetup_OnPartyAdd(object sender, PartyEvents e)
    {
        OnPartyAdd?.Invoke(sender, e);
    }


    private void PartySetup_OnPartyRemove(object sender, PartyEvents e)
    {
        OnPartyRemove?.Invoke(sender, e);
    }


    public void OnDestroy()
    {
        for (int i = 0; i < PartySetupList.Count; i++)
        {
            PartySetup partySetup = PartySetupList[i];
            partySetup.OnPartyAdd -= PartySetup_OnPartyAdd;
            partySetup.OnPartyRemove -= PartySetup_OnPartyRemove;
        }
    }

    public PartySetup GetParty(int PartyIndex)
    {
        PartyIndex = Mathf.Clamp(PartyIndex, 0, GetPartySetupSize());

        return PartySetupList[PartyIndex];
    }

    private int GetPartySetupSize()
    {
        return PartySetupList.Count;
    }

    public void SetCurrentParty(int PartyLocation)
    {
        currentPartySetup = GetParty(PartyLocation);
        OnCurrentPartyChanged?.Invoke();
    }


    public void AddMember(CharacterDataStat CharacterDataStat, int PartySetupIndex, int PartyLocation)
    {
        GetParty(PartySetupIndex).AddMember(CharacterDataStat, PartyLocation);
    }

    public void AddMember(CharacterDataStat CharacterDataStat, int PartySetupIndex)
    {
        GetParty(PartySetupIndex).AddMember(CharacterDataStat);
    }


    public void RemoveMember(int PartyLocation, int PartySetupIndex)
    {
        GetParty(PartySetupIndex).RemoveMember(PartyLocation);
    }

    public void RemoveMember(CharactersSO CharactersSO, int PartySetupIndex)
    {
        GetParty(PartySetupIndex).RemoveMember(CharactersSO);
    }

    public void RemoveAllMembers(CharactersSO CharactersSO)
    {
        for (int i = 0; i < PartySetupList.Count; i++)
        {
            RemoveMember(CharactersSO, i);
        }
    }
}
