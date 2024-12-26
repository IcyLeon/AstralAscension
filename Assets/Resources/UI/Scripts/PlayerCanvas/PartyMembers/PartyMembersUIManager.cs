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
    private CharacterStorage characterStorage;
    private Dictionary<PartyMember, PartyMemberContent> PartyMemberContentDictionary;

    //Start is called before the first frame update
    private void Awake()
    {
        objectPool = new ObjectPool<PartyMemberContent>(PartyInfoPrefab, transform, 5);
        objectPool.CallbackPoolObject((p, i) => p.SetIndexKeyText(i + 1));
        PartyMemberContentDictionary = new();
        CharacterManager.OnCharacterStorageOld += CharacterManager_OnCharacterStorageOld;
        CharacterManager.OnCharacterStorageNew += CharacterManager_OnCharacterStorageNew;
    }

    private void CharacterManager_OnCharacterStorageOld(CharacterStorage CharacterStorage)
    {
    }

    private void CharacterManager_OnCharacterStorageNew(CharacterStorage CharacterStorage)
    {
        characterStorage = CharacterStorage;
    }

    private void Start()
    {
        InitCharacterStorage();
        InitPartySetup();
    }

    private void InitPartySetup()
    {
        partySystem = PartySystem.instance;

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

    private void InitCharacterStorage()
    {
        if (characterStorage != null)
            return;

        CharacterManager_OnCharacterStorageNew(CharacterManager.instance.characterStorage);
    }


    private void PartySystem_OnActivePartyChanged()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        PartyMemberContentDictionary.Clear();
        objectPool.ResetAll();

        foreach (var partySlot in currentPartySetup.GetKnownPartySlots())
        {
            PartyMemberContent partyMemberPoolObject = objectPool.GetPooledObject();

            if (partyMemberPoolObject == null)
                continue;

            CharacterDataStat CharacterDataStat = characterStorage.GetCharacterDataStat(partySlot.partyMember.charactersSO);

            partyMemberPoolObject.SetCharacterDataStat(CharacterDataStat);

            if (!PartyMemberContentDictionary.TryGetValue(partySlot.partyMember, out PartyMemberContent partyMemberContent))
            {
                PartyMemberContentDictionary.Add(partySlot.partyMember, partyMemberPoolObject);
            }
        }
    }


    private void OnDestroy()
    {
        CharacterManager.OnCharacterStorageOld -= CharacterManager_OnCharacterStorageOld;
        CharacterManager.OnCharacterStorageNew -= CharacterManager_OnCharacterStorageNew;

        if (partySystem == null)
        {
            partySystem.OnActivePartyChanged -= PartySystem_OnActivePartyChanged;
        }
    }
}
