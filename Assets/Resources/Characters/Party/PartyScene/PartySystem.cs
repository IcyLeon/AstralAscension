using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PartySystem : MonoBehaviour
{
    public static PartySystem instance { get; private set; }
    public PartySetupManager mainPartySetupManagerData { get; private set; }
    public PartySetup activePartySetup { get; private set; }
    public event Action OnActivePartyChanged;
    public event Action OnMainDataChanged;

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
        mainPartySetupManagerData = new(4);
        TestAddMembers();
        OnMainDataChanged?.Invoke();
    }

    private void SetFirstActiveParty()
    {
        SetActiveParty(mainPartySetupManagerData.currentPartySetup);
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
            mainPartySetupManagerData.AddMember(c.characterStatList.ElementAt(i).Value, 0);
        }
    }

    private void OnDestroy()
    {
        mainPartySetupManagerData.OnDestroy();
    }
}
