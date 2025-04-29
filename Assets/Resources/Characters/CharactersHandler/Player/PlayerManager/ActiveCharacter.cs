using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static PartySetupManager;

[DisallowMultipleComponent]
public class ActiveCharacter : MonoBehaviour
{
    public delegate void OnPlayerCharacterEvent(PartyMember PrevPartyMember, PartyMember NewPartyMember);
    public event OnPlayerCharacterEvent OnPlayerCharacterSwitch;

    private Player player;
    private PlayerController playerController;

    private Dictionary<PartyMember, PlayableCharacters> charactersList;
    private PartySystem partySystem;
    private PartySetup currentPartySetup;
    private PartySlot currentPartyMemberSlot;
    public PartyMember currentPartyMember { get; private set; }

    private void Awake()
    {
        charactersList = new();
        player = GetComponentInParent<Player>();
    }

    private void Start()
    {
        playerController = player.playerController;
        playerController.playerInputAction.SwitchCharacters.performed += SwitchCharacters_performed;

        InitPartySetup();
    }

    private void UnsubscribePartyEvents()
    {
        if (currentPartySetup == null)
            return;

        currentPartySetup.OnPartyAdd -= PartySetup_OnPartyAdd;
        currentPartySetup.OnPartyRemove -= PartySetup_OnPartyRemove;
    }

    private void SubscribePartyEvents()
    {
        if (currentPartySetup == null)
            return;

        currentPartySetup.OnPartyAdd += PartySetup_OnPartyAdd;
        currentPartySetup.OnPartyRemove += PartySetup_OnPartyRemove;
        OnPartyChanged();
    }

    private void InitPartySetup()
    {
        partySystem = PartySystem.instance;

        if (partySystem == null)
            return;

        partySystem.OnActivePartyChanged += PartySystem_OnCurrentActivePartyChanged;
        UpdatePartySetup();
    }

    private void UpdatePartySetup()
    {
        if (partySystem == null)
            return;

        UnsubscribePartyEvents();
        currentPartySetup = partySystem.activePartySetup;
        SubscribePartyEvents();
    }

    private void PartySystem_OnCurrentActivePartyChanged()
    {
        UpdatePartySetup();
    }

    private void PartySetup_OnPartyRemove(object sender, PartyEvents e)
    {
        RemoveMember(e.PartyMemberSlot.partyMember);
    }

    private void PartySetup_OnPartyAdd(object sender, PartyEvents e)
    {
        AddMember(e.PartyMemberSlot.partyMember);
    }

    private void RemoveMember(PartyMember PartyMember)
    {
        if (!charactersList.TryGetValue(PartyMember, out PlayableCharacters playableCharacters))
            return;

        Destroy(playableCharacters.gameObject);
        charactersList.Remove(PartyMember);
    }

    private void AddMember(PartyMember PartyMember)
    {
        DamageableCharacters DamageableCharacters = PartyMember.characterDataStat.damageableEntitySO.CreateCharacter(transform);

        if (DamageableCharacters == null)
            return;

        DamageableCharacters.SetCharacterDataStat(PartyMember.characterDataStat);
        DamageableCharacters.gameObject.SetActive(false);
        charactersList.Add(PartyMember, DamageableCharacters as PlayableCharacters);
    }

    private void OnPartyChanged()
    {
        for (int i = charactersList.Count - 1; i >= 0; i--)
        {
            RemoveMember(charactersList.ElementAt(i).Key);
        }
        charactersList.Clear();

        List<PartySlot> MemberList = currentPartySetup.GetKnownPartySlots();

        foreach (var partySlot in MemberList)
        {
            AddMember(partySlot.partyMember);
        }

        SwitchCharacter(currentPartySetup.GetSelectedPartyMemberSlotIndex(), false);
    }

    private void SwitchCharacters_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        int.TryParse(obj.control.name, out int numKeyValue);

        if (currentPartySetup == null)
            return;

        int actualValue = numKeyValue - 1;

        SwitchCharacter(actualValue);
    }

    public bool CanSwitchCharacter(PlayableCharacters pc)
    {
        if (pc == null)
            return true;

        return !pc.PlayableCharacterStateMachine.IsSkillCasting() && !pc.PlayableCharacterStateMachine.IsInState<PlayerAirborneState>();
    }

    private void SwitchCharacter(int index, bool playSwitchSound = true)
    {
        if (index < 0 || index >= charactersList.Count)
            return;

        PartySlot nextPartyMemberSlot = currentPartySetup.GetPartySlot(index);

        if (nextPartyMemberSlot == currentPartyMemberSlot)
            return;

        currentPartyMemberSlot = currentPartySetup.selectedPartyMemberSlot;

        if (currentPartyMemberSlot.partyMember != null)
        {
            PlayableCharacters currentPlayableCharacter = GetPlayableCharacter(GetPartyMember(currentPartyMemberSlot));

            if (!CanSwitchCharacter(currentPlayableCharacter))
            {
                return;
            }

            currentPlayableCharacter.gameObject.SetActive(false);
        }

        PartySlot prevPartySlot = currentPartyMemberSlot;
        currentPartySetup.SelectPartyMemberSlot(index);
        currentPartyMemberSlot = currentPartySetup.selectedPartyMemberSlot;
        currentPartyMember = GetPartyMember(currentPartyMemberSlot);
        PlayableCharacters newPlayableCharacter = GetPlayableCharacter(currentPartyMember);
        newPlayableCharacter.gameObject.SetActive(true);
        OnPlayerCharacterSwitch?.Invoke(GetPartyMember(prevPartySlot), currentPartyMember);


        if (playSwitchSound)
        {
            player.PlayPlayerSoundEffect(player.PlayerSO.SoundData.SwitchClip);
        }
    }

    private PartyMember GetPartyMember(PartySlot PartySlot)
    {
        if (PartySlot == null)
            return null;

        return PartySlot.partyMember;
    }

    private PlayableCharacters GetPlayableCharacter(PartyMember PartyMember)
    {
        if (PartyMember == null || !charactersList.TryGetValue(PartyMember, out PlayableCharacters pc))
            return null;

        return pc;
    }

    private void OnDestroy()
    {
        UnsubscribePartyEvents();
        playerController.playerInputAction.SwitchCharacters.performed -= SwitchCharacters_performed;

        if (partySystem != null)
        {
            partySystem.OnActivePartyChanged -= PartySystem_OnCurrentActivePartyChanged;
        }
    }

}
