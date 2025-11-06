using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(AudioSource))]
public class ActiveCharacter : MonoBehaviour
{
    private AudioSource switchCharacterAudioSource;
    private PlayerController playerController;
    private PartySetup currentPartySetup;
    private PartyMember currentPartyMember;
    private Dictionary<PartyMember, DamageableCharacters> charactersList;
    private PartySystem partySystem;

    private void Awake()
    {
        charactersList = new();
        Player player = GetComponentInParent<Player>();
        switchCharacterAudioSource = GetComponent<AudioSource>();
        playerController = player.playerController;
    }

    private void OnEnable()
    {
        SubscribeInputEvents();
    }

    private void OnDisable()
    {
        UnsubscribeInputEvents();
    }

    private void Start()
    {
        partySystem = PartySystem.instance;
        SubscribeActivePartyEvents();
    }

    private void SubscribeInputEvents()
    {
        playerController.playerInputAction.SwitchCharacters.performed += SwitchCharacters_performed;
    }

    private void UnsubscribeInputEvents()
    {
        playerController.playerInputAction.SwitchCharacters.performed -= SwitchCharacters_performed;
    }
    private void SubscribeActivePartyEvents()
    {
        if (partySystem == null)
            return;

        partySystem.OnActivePartyChanged += PartySystem_OnActivePartyChanged;
        UpdateCurrentParty();

    }
    private void UnsubscribeActivePartyEvents()
    {
        if (partySystem == null)
            return;

        partySystem.OnActivePartyChanged -= PartySystem_OnActivePartyChanged;
    }

    private bool CanSwitchCharacter(PlayableCharacters pc)
    {
        if (pc == null || pc.playableCharacterStateMachine == null)
            return true;

        return !pc.playableCharacterStateMachine.IsSkillCasting() && !pc.playableCharacterStateMachine.IsInState<PlayerAirborneState>();
    }

    private void PartySystem_OnActivePartyChanged()
    {
        UpdateCurrentParty();
    }

    private void UpdateCurrentParty()
    {
        UnsubscribeCurrentParty();
        currentPartySetup = partySystem.activePartySetup;
        SubscribeCurrentParty();
    }

    private void UnsubscribeCurrentParty()
    {
        if (currentPartySetup == null)
            return;

        currentPartySetup.OnPartyAdd -= CurrentPartySetup_OnPartyAdd;
        currentPartySetup.OnPartyRemove -= CurrentPartySetup_OnPartyRemove;
        currentPartySetup.OnSelectPartyMemberChanged -= CurrentPartySetup_OnSelectPartyMemberChanged;
    }
    private void SubscribeCurrentParty()
    {
        if (currentPartySetup == null)
            return;

        currentPartySetup.OnPartyAdd += CurrentPartySetup_OnPartyAdd;
        currentPartySetup.OnPartyRemove += CurrentPartySetup_OnPartyRemove;
        currentPartySetup.OnSelectPartyMemberChanged += CurrentPartySetup_OnSelectPartyMemberChanged;
        OnPartyChanged();
    }

    private void CurrentPartySetup_OnPartyAdd(object sender, PartyEvents e)
    {
        AddMember(e.PartyMemberSlot.partyMember);
    }
    private void CurrentPartySetup_OnPartyRemove(object sender, PartyEvents e)
    {
        RemoveMember(e.PartyMemberSlot.partyMember);
    }

    private void OnPartyChanged()
    {
        foreach(var partyMember in charactersList.Keys.ToList())
        {
            RemoveMember(partyMember);
        }

        foreach (var partySlot in currentPartySetup.GetKnownPartySlots())
        {
            AddMember(partySlot.partyMember);
        }

        currentPartySetup.SelectPartyMemberSlot(0);
    }

    private void RemoveMember(PartyMember PartyMember)
    {
        DamageableCharacters characters = GetPlayableCharacter(PartyMember);

        if (characters == null)
            return;

        Destroy(characters.gameObject);
        charactersList.Remove(PartyMember);
    }
    private void AddMember(PartyMember PartyMember)
    {
        DamageableCharacters existCharacters = GetPlayableCharacter(PartyMember);

        if (existCharacters != null)
            return;

        DamageableCharacters DamageableCharacters = PartyMember.characterDataStat.damageableEntitySO.CreateCharacter(transform);

        if (DamageableCharacters == null)
            return;

        DamageableCharacters.SetCharacterDataStat(PartyMember.characterDataStat);
        DamageableCharacters.gameObject.SetActive(false);
        charactersList.Add(PartyMember, DamageableCharacters);
    }

    private void SwitchCharacters_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (currentPartySetup == null)
            return;

        int.TryParse(obj.control.name, out int numKeyValue);

        int partyIndex = numKeyValue - 1;

        currentPartySetup.SelectPartyMemberSlot(partyIndex);
    }

    private void CurrentPartySetup_OnSelectPartyMemberChanged()
    {
        SwitchCharacter(GetPartyMember(currentPartySetup.selectedPartyMemberSlot), true);
    }

    private PartyMember GetPartyMember(PartySlot PartySlot)
    {
        if (PartySlot == null) 
            return null;

        return PartySlot.partyMember;
    }

    private DamageableCharacters GetPlayableCharacter(PartyMember PartyMember)
    {
        if (PartyMember == null || !charactersList.TryGetValue(PartyMember, out DamageableCharacters characters))
            return null;

        return characters;
    }

    private void SwitchCharacter(PartyMember PartyMember, bool playSound = false)
    {
        if (currentPartyMember == PartyMember)
            return;

        DamageableCharacters character = GetPlayableCharacter(currentPartyMember);

        if (currentPartyMember != null)
        {
            if (!CanSwitchCharacter(character as PlayableCharacters))
                return;

            character.gameObject.SetActive(false);
        }

        currentPartyMember = PartyMember;
        character = GetPlayableCharacter(currentPartyMember);
        character.gameObject.SetActive(true);
        CharacterSwitchEvent.Switch(currentPartyMember);

        if (playSound)
        {
            switchCharacterAudioSource.PlayOneShot(switchCharacterAudioSource.clip);
        }
    }

    private void OnDestroy()
    {
        UnsubscribeCurrentParty();
        UnsubscribeActivePartyEvents();
        UnsubscribeInputEvents();
    }

}