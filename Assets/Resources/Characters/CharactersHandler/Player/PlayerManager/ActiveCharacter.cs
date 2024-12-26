using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static PartySetupManager;

[DisallowMultipleComponent]
public class ActiveCharacter : MonoBehaviour
{
    public delegate void OnPlayerCharacterEvent(CharacterDataStat playerData, PartyMember PartyMember);
    public static event OnPlayerCharacterEvent OnPlayerCharacterExit;
    public static event OnPlayerCharacterEvent OnPlayerCharacterSwitch;

    private Player player;
    private PlayerController playerController;

    private Dictionary<PartyMember, PlayableCharacters> charactersList;
    private PartySystem partySystem;
    private PartySetup currentPartySetup;
    private PartySlot currentPartyMemberSlot;

    private void Awake()
    {
        charactersList = new();
        player = GetComponentInParent<Player>();
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
        DamageableCharacters DamageableCharacters = PartyMember.charactersSO.CreateCharacter(transform);

        if (DamageableCharacters == null)
            return;

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

    // Start is called before the first frame update
    private void Start()
    {
        InitPartySetup();
        playerController = PlayerController.instance;
        playerController.playerInputAction.SwitchCharacters.performed += SwitchCharacters_performed;
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

        return !pc.PlayableCharacterStateMachine.IsSkillCasting() && !pc.PlayableCharacterStateMachine.playerStateMachine.IsInState<PlayerAirborneState>();
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
            PlayableCharacters currentPlayableCharacter = GetPlayableCharacter(currentPartyMemberSlot.partyMember);

            if (!CanSwitchCharacter(currentPlayableCharacter))
            {
                return;
            }

            currentPlayableCharacter.gameObject.SetActive(false);
            OnPlayerCharacterExit?.Invoke(currentPlayableCharacter.GetCharacterDataStat(), currentPartyMemberSlot.partyMember);
        }

        currentPartySetup.SelectPartyMemberSlot(index);
        currentPartyMemberSlot = currentPartySetup.selectedPartyMemberSlot;
        PlayableCharacters newPlayableCharacter = GetPlayableCharacter(currentPartyMemberSlot.partyMember);
        newPlayableCharacter.gameObject.SetActive(true);
        OnPlayerCharacterSwitch?.Invoke(newPlayableCharacter.GetCharacterDataStat(), currentPartyMemberSlot.partyMember);


        if (playSwitchSound)
        {
            player.PlayPlayerSoundEffect(player.PlayerSO.SoundData.SwitchClip);
        }
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
