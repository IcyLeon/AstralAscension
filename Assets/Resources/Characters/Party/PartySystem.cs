using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PartySystem : MonoBehaviour
{
    public static PartySystem instance { get; private set; }

    private PartySetupManager mainPartySetupManager;
    public event Action OnActivePartyChanged;
    public PartySetup activePartySetup { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CreateMainGameplayParties();
        SetFirstActiveParty();
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
            mainPartySetupManager.AddMember(c.characterStatList.ElementAt(i).Key, 0);
        }
    }

    private void OnDestroy()
    {
        mainPartySetupManager.OnDestroy();
    }

}
