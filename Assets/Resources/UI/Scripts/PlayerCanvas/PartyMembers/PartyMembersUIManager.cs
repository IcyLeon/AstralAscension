using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PartySetupManager;

[DisallowMultipleComponent]
public class PartyMembersUIManager : MonoBehaviour
{
    private ObjectPool<PartyMemberContent> objectPool;
    [SerializeField] private GameObject PartyInfoPrefab;
    private PartySystem partySystem;
    private PartySetup currentPartySetup;
    private Dictionary<PartyMember, PartyMemberContent> partyMemberContentDictionary = new();

    //Start is called before the first frame update
    private void Awake()
    {
        objectPool = new ObjectPool<PartyMemberContent>(PartyInfoPrefab, transform, 5);
        objectPool.CallbackPoolObject((p, i) => p.SetIndexKeyText(i + 1));
    }

    private void Start()
    {
        partySystem = PartySystem.instance;
        InitPartySetup();
    }

    private void InitPartySetup()
    {
        if (partySystem == null)
            return;

        partySystem.OnActivePartyChanged += PartySystem_OnActivePartyChanged;
        UpdatePartySetup();
    }

    private void UpdatePartySetup()
    {
        currentPartySetup = partySystem.activePartySetup;
        UpdateVisual();
    }


    private void PartySystem_OnActivePartyChanged()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        partyMemberContentDictionary.Clear();
        objectPool.ResetAll();

        foreach (var partySlot in currentPartySetup.GetKnownPartySlots())
        {
            PartyMemberContent partyMemberPoolObject = objectPool.GetPooledObject();

            if (partyMemberPoolObject == null)
                continue;

            partyMemberPoolObject.SetCharacterDataStat(partySlot.partyMember.characterDataStat);

            if (!partyMemberContentDictionary.TryGetValue(partySlot.partyMember, out PartyMemberContent partyMemberContent))
            {
                partyMemberContentDictionary.Add(partySlot.partyMember, partyMemberPoolObject);
            }
        }
    }


    private void OnDestroy()
    {
        if (partySystem == null)
        {
            partySystem.OnActivePartyChanged -= PartySystem_OnActivePartyChanged;
        }
    }
}
