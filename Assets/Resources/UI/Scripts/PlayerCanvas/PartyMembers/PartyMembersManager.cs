using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static CharacterManager;

[DisallowMultipleComponent]
public class PartyMembersManager : MonoBehaviour
{
    private ObjectPool<PartyMemberContent> objectPool;
    [SerializeField] private GameObject PartyInfoPrefab;
    private CharacterStorage characterStorage;
    private Dictionary<CharacterDataStat, PartyMemberContent> PartyMemberContentDictionary;

    //Start is called before the first frame update
    private void Awake()
    {
        objectPool = new ObjectPool<PartyMemberContent>(PartyInfoPrefab, transform, MAX_EQUIP_CHARACTERS);
        objectPool.CallbackPoolObject((PartyMemberContent, i) => PartyMemberContent.SetIndexKeyText(i + 1));
        PartyMemberContentDictionary = new(); // for add and remove events
        OnCharacterStorageOld += CharacterManager_OnCharacterStorageOld;
        OnCharacterStorageNew += CharacterManager_OnCharacterStorageNew;
    }

    private void CharacterManager_OnCharacterStorageOld(CharacterStorage CharacterStorage)
    {
        CharacterStorage.characterEquippedManager.OnEquipChanged -= CharacterEquippedManager_OnEquipChanged;
    }


    private void CharacterManager_OnCharacterStorageNew(CharacterStorage CharacterStorage)
    {
        characterStorage = CharacterStorage;
        if (characterStorage != null)
        {
            characterStorage.characterEquippedManager.OnEquipChanged += CharacterEquippedManager_OnEquipChanged;
            UpdateVisual();
        }
    }

    private void CharacterEquippedManager_OnEquipChanged(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        objectPool.ResetAll();

        CharacterEquippedManager characterEquippedManager = characterStorage.characterEquippedManager;

        foreach (var characterDataStat in characterEquippedManager.GetEquippedCharacterStat())
        {
            PartyMemberContent partyMemberContent = objectPool.GetPooledObject();

            if (partyMemberContent == null)
                continue;

            partyMemberContent.SetCharacterDataStat(characterDataStat as PlayableCharacterDataStat);
        }
    }

    /// ///////////////////////////////////////////////////////////////////
    // This is if u want to use OnEquipAdd and remove events instead of the equip change event
    //
    //private void CharacterManager_OnCharacterStorageOld(CharacterStorage CharacterStorage)
    //{
    //    CharacterStorage.characterEquippedManager.OnEquipAdd -= CharacterEquippedManager_OnEquipAdd;
    //    CharacterStorage.characterEquippedManager.OnEquipRemove -= CharacterEquippedManager_OnEquipRemove;
    //}

    //private void CharacterManager_OnCharacterStorageNew(CharacterStorage CharacterStorage)
    //{
    //    characterStorage = CharacterStorage;
    //    if (characterStorage != null)
    //    {
    //        characterStorage.characterEquippedManager.OnEquipAdd += CharacterEquippedManager_OnEquipAdd;
    //        characterStorage.characterEquippedManager.OnEquipRemove += CharacterEquippedManager_OnEquipRemove;
    //        ResetVisual();
    //    }
    //}

    //private void CharacterEquippedManager_OnEquipAdd(object sender, CharacterEquippedManager.CharacterEquipEvents e)
    //{
    //    if (PartyMemberContentDictionary.ContainsKey(e.CharacterDataStat))
    //        return;

    //    PartyMemberContent p = objectPool.GetPooledObject();
    //    if (p == null)
    //        return;

    //    PartyMemberContentDictionary.Add(e.CharacterDataStat, p);
    //    UpdateVisual();
    //}

    //private void CharacterEquippedManager_OnEquipRemove(object sender, CharacterEquippedManager.CharacterEquipEvents e)
    //{
    //    if (!PartyMemberContentDictionary.ContainsKey(e.CharacterDataStat))
    //        return;

    //    objectPool.Reset(PartyMemberContentDictionary[e.CharacterDataStat]);
    //    PartyMemberContentDictionary.Remove(e.CharacterDataStat);
    //    UpdateVisual();
    //}

    //private void UpdateVisual()
    //{
    //    CharacterEquippedManager characterEquippedManager = characterStorage.characterEquippedManager;
    //    int i = 0;
    //    foreach (var characterDataStat in characterEquippedManager.GetEquippedCharacterStat())
    //    {
    //        if (PartyMemberContentDictionary.TryGetValue(characterDataStat, out PartyMemberContent PartyMemberContent))
    //        {
    //            Debug.Log(characterDataStat.damageableEntitySO);
    //            PartyMemberContent.SetCharacterDataStat(characterDataStat as PlayableCharacterDataStat);
    //            PartyMemberContent.SetIndexKeyText(i + 1);
    //            i++;
    //        }
    //    }
    //}
    /////////////////////////////////////////////////////////////////////

    private void ResetVisual()
    {
        PartyMemberContentDictionary.Clear();
        objectPool.ResetAll();

        CharacterEquippedManager characterEquippedManager = characterStorage.characterEquippedManager;
        if (characterEquippedManager == null)
            return;

        foreach (var characterDataStat in characterEquippedManager.GetEquippedCharacterStat())
        {
            PartyMemberContent partyMemberContent = objectPool.GetPooledObject();

            if (partyMemberContent == null)
                continue;

            partyMemberContent.SetCharacterDataStat(characterDataStat as PlayableCharacterDataStat);
            PartyMemberContentDictionary.Add(characterDataStat, partyMemberContent);
        }
    }

    private void OnDestroy()
    {
        OnCharacterStorageOld -= CharacterManager_OnCharacterStorageOld;
        OnCharacterStorageNew -= CharacterManager_OnCharacterStorageNew;
        if (characterStorage != null)
        {
            characterStorage.characterEquippedManager.OnEquipChanged -= CharacterEquippedManager_OnEquipChanged;
            //characterStorage.characterEquippedManager.OnEquipAdd -= CharacterEquippedManager_OnEquipAdd;
            //characterStorage.characterEquippedManager.OnEquipRemove -= CharacterEquippedManager_OnEquipRemove;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
