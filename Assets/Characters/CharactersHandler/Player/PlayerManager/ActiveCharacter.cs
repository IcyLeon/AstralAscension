using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static CharacterManager;

[DisallowMultipleComponent]
public class ActiveCharacter : MonoBehaviour
{
    public delegate void OnPlayerCharacterEvent(CharacterDataStat playerData, PlayableCharacters playableCharacters);
    public static event OnPlayerCharacterEvent OnPlayerCharacterExit;
    public static event OnPlayerCharacterEvent OnPlayerCharacterSwitch;

    private Player player;

    private CharacterDataStat currentPlayableCharacterData;

    private CharacterStorage characterStorage;

    private void Awake()
    {
        OnCharacterStorageNew += ActiveCharacter_OnCharacterStorageNew;
        OnCharacterStorageOld += ActiveCharacter_OnCharacterStorageOld;

        PartyMemberContent.PartyMemberClick += PartyMemberContent_PartyMemberClick;

        player = GetComponentInParent<Player>();
    }

    private void PartyMemberContent_PartyMemberClick(CharacterDataStat CharacterDataStat)
    {
        SwitchCharacter(CharacterDataStat);
    }

    private void ActiveCharacter_OnCharacterStorageOld(CharacterStorage CharacterStorage)
    {
    }

    private void ActiveCharacter_OnCharacterStorageNew(CharacterStorage CharacterStorage)
    {
        characterStorage = CharacterStorage;
        if (characterStorage != null)
        {
        }
    }

    public PlayableCharacters GetPlayableCharacters(CharacterDataStat c)
    {
        foreach (var p in GetAllPlayableCharacters())
        {
            if (p.GetCharacterDataStat() == c)
                return p;
        }
        return null;
    }

    // Start is called before the first frame update
    private void Start()
    {
        player.PlayerController.playerInputAction.SwitchCharacters.performed += SwitchCharacters_performed;
        player.PlayerController.playerInputAction.BowAim.started += BowAim_started;
        InitExistingCharacters();
    }

    private void BowAim_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        PlayableCharacterDataStat p = currentPlayableCharacterData as PlayableCharacterDataStat;
        p.AddEnergy(10);
    }

    private void InitExistingCharacters()
    {
        if (characterStorage == null || characterStorage.characterEquippedManager == null)
            return;

        CharacterEquippedManager characterEquippedManager = characterStorage.characterEquippedManager;

        PlayableCharacters[] pcList = GetAllPlayableCharacters();
        for (int i = 0; i < pcList.Length; i++)
        {
            PlayableCharacters pc = pcList[i];
            characterEquippedManager.AddEquipPlayableCharacterToList(pc.GetCharacterDataStat());
            characterStorage.AddCharacterData(pc.GetCharacterDataStat());
        }

        PlayableCharacters existPlayableCharacters = GetComponentInChildren<PlayableCharacters>();
        if (existPlayableCharacters != null)
        {
            SwitchCharacter(existPlayableCharacters.GetCharacterDataStat(), false);
        }
    }


    private void SwitchCharacters_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        int.TryParse(obj.control.name, out int numKeyValue);

        if (numKeyValue > MAX_EQUIP_CHARACTERS || characterStorage == null)
            return;

        int actualValue = numKeyValue - 1;

        SwitchCharacter(actualValue);
    }

    public PlayableCharacters[] GetAllPlayableCharacters()
    {
        return GetComponentsInChildren<PlayableCharacters>(true);
    }

    public bool CanSwitchCharacter(PlayableCharacters pc)
    {
        if (pc == null)
            return false;

        return !pc.PlayableCharacterStateMachine.IsSkillCasting() && !pc.PlayableCharacterStateMachine.playerStateMachine.IsInState<PlayerAirborneState>();
    }

    private void SwitchCharacter(int index, bool playSwitchSound = true)
    {
        if (characterStorage == null || characterStorage.characterEquippedManager == null)
            return;

        CharacterEquippedManager characterEquippedManager = characterStorage.characterEquippedManager;

        List<CharacterDataStat> list = characterEquippedManager.GetEquippedCharacterStat();

        if (index >= list.Count)
            return;

        SwitchCharacter(list[index], playSwitchSound);
    }

    private void SwitchCharacter(CharacterDataStat PlayableCharacterDataStat, bool playSwitchSound = true)
    {
        if (PlayableCharacterDataStat == null || currentPlayableCharacterData == PlayableCharacterDataStat)
            return;

        PlayableCharacters currentPlayableCharacter = GetPlayableCharacters(currentPlayableCharacterData);

        if (currentPlayableCharacterData != null)
        {
            if (!CanSwitchCharacter(currentPlayableCharacter))
                return;

            foreach (var characters in GetAllPlayableCharacters())
            {
                characters.gameObject.SetActive(false);
            }
            OnPlayerCharacterExit?.Invoke(currentPlayableCharacterData, currentPlayableCharacter);
        }

        currentPlayableCharacterData = PlayableCharacterDataStat;
        currentPlayableCharacter = GetPlayableCharacters(currentPlayableCharacterData);


        currentPlayableCharacter.gameObject.SetActive(true);
        OnPlayerCharacterSwitch?.Invoke(currentPlayableCharacterData, currentPlayableCharacter);

        if (playSwitchSound)
            player.PlayPlayerSoundEffect(player.PlayerSO.SoundData.SwitchClip);
    }

    private void OnDestroy()
    {
        OnCharacterStorageNew -= ActiveCharacter_OnCharacterStorageNew;
        PartyMemberContent.PartyMemberClick -= PartyMemberContent_PartyMemberClick;
        OnCharacterStorageOld -= ActiveCharacter_OnCharacterStorageOld;
        if (characterStorage != null)
        {
        }

        player.PlayerController.playerInputAction.SwitchCharacters.performed -= SwitchCharacters_performed;
    }

    // Update is called once per frame
    private void Update()
    {
    }
}
