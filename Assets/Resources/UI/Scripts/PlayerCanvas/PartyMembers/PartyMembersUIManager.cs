using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PartySetupManager;

[DisallowMultipleComponent]
public class PartyMembersUIManager : MonoBehaviour
{
    private ObjectPool<PartyMemberContent> objectPool;
    [SerializeField] private GameObject PartyInfoPrefab;
    private PartySetupManager partySetupManager;
    private Dictionary<CharacterDataStat, PartyMemberContent> PartyMemberContentDictionary;

    //Start is called before the first frame update
    private void Awake()
    {
        objectPool = new ObjectPool<PartyMemberContent>(PartyInfoPrefab, transform, MAX_EQUIP_CHARACTERS);
        objectPool.CallbackPoolObject((p, i) => p.SetIndexKeyText(i + 1));
        PartyMemberContentDictionary = new();
        CharacterManager.OnCharacterStorageOld += CharacterManager_OnCharacterStorageOld;
        CharacterManager.OnCharacterStorageNew += CharacterManager_OnCharacterStorageNew;
    }

    private void CharacterManager_OnCharacterStorageOld(CharacterStorage CharacterStorage)
    {
        CharacterStorage.PartySetupManager.OnCurrentPartyChanged -= PartySetupManager_OnCurrentPartyChanged;
    }

    private void CharacterManager_OnCharacterStorageNew(CharacterStorage CharacterStorage)
    {
        if (CharacterStorage != null)
        {
            partySetupManager = CharacterStorage.PartySetupManager;
            partySetupManager.OnCurrentPartyChanged += PartySetupManager_OnCurrentPartyChanged;
            UpdateVisual();
        }
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        if (partySetupManager != null)
            return;

        CharacterManager_OnCharacterStorageNew(CharacterManager.instance.characterStorage);
    }

    private void PartySetupManager_OnCurrentPartyChanged()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        PartyMemberContentDictionary.Clear();
        objectPool.ResetAll();

        foreach (var characterDataStat in partySetupManager.GetCurrentPartyMembers())
        {
            if (characterDataStat == null)
                continue;

            PartyMemberContent partyMemberContent = objectPool.GetPooledObject();

            if (partyMemberContent == null)
                continue;

            PlayableCharacterDataStat pc = partySetupManager.GetPlayableCharacterDataStat(characterDataStat);

            partyMemberContent.SetCharacterDataStat(pc);

            if (!PartyMemberContentDictionary.ContainsKey(pc))
            {
                PartyMemberContentDictionary.Add(pc, partyMemberContent);
            }
        }
    }


    private void OnDestroy()
    {
        CharacterManager.OnCharacterStorageOld -= CharacterManager_OnCharacterStorageOld;
        CharacterManager.OnCharacterStorageNew -= CharacterManager_OnCharacterStorageNew;
        if (partySetupManager != null)
        {
            partySetupManager.OnCurrentPartyChanged -= PartySetupManager_OnCurrentPartyChanged;
        }
    }
}
