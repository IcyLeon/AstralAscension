using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PartySystem
{
    private static PartySystem partySystemInstance;
    private PartySetupManager mainPartySetupManager;
    public event Action OnActivePartyChanged;
    public PartySetup activePartySetup { get; private set; }

    public static PartySystem instance
    {
        get
        {
            return GetInstance();
        }
    }

    private PartySystem()
    {
        CreateMainGameplayParties();
        SetFirstActiveParty();
    }

    private static PartySystem GetInstance()
    {
        if (partySystemInstance == null)
        {
            partySystemInstance = new PartySystem();
        }

        return partySystemInstance;
    }

    private void CreateMainGameplayParties()
    {
        mainPartySetupManager = new(4);
        TestAddMembers();
    }

    private void SetFirstActiveParty()
    {
        SetActiveParty(mainPartySetupManager.currentPartySetup);
    }

    public void SetActiveParty(PartySetup PartySetup)
    {
        if (activePartySetup == PartySetup)
            return;

        activePartySetup = PartySetup;
        OnActivePartyChanged?.Invoke();
    }

    private void TestAddMembers()
    {
        CharacterStorage c = CharacterManager.instance.characterStorage;
        for (int i = 0; i < c.characterStatList.Count; i++)
        {
            mainPartySetupManager.AddMember(c.characterStatList.ElementAt(i).Value, 0);
        }
    }

    private void OnDestroy()
    {
        mainPartySetupManager.OnDestroy();
    }

}
