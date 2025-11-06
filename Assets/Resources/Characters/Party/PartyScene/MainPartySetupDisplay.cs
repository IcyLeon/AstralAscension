using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPartySetupDisplay : MonoBehaviour
{
    private PartySystem partySystem;
    private PartySetupManager mainPartySetupManagerData;
    private int currentPartyIdx;

    private void Awake()
    {
        currentPartyIdx = 0;
    }

    // Start is called before the first frame update
    private void Start()
    {
        partySystem = PartySystem.instance;
        SubscribeEvents();
    }

    private void PartySystem_OnMainDataChanged()
    {
        UpdateMainData();
    }

    private void SubscribeEvents()
    {
        if (partySystem == null)
            return;

        partySystem.OnMainDataChanged += PartySystem_OnMainDataChanged;
        UpdateMainData();
    }
    private void UnsubscribeEvents()
    {
        if (partySystem == null)
            return;

        partySystem.OnMainDataChanged -= PartySystem_OnMainDataChanged;
    }

    private void UpdateMainData()
    {
        mainPartySetupManagerData = partySystem.mainPartySetupManagerData;
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }
}
