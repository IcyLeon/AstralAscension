using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static PartySetupManager;

[DisallowMultipleComponent]
public class ActiveCharacter : MonoBehaviour
{
    public delegate void OnPlayerCharacterEvent(CharacterDataStat playerData, PlayableCharacters playableCharacters);
    public static event OnPlayerCharacterEvent OnPlayerCharacterExit;
    public static event OnPlayerCharacterEvent OnPlayerCharacterSwitch;

    private Player player;

    private Dictionary<CharactersSO, PlayableCharacters> charactersList;
    private CharacterDataStat currentPlayableCharacterData;
    private PartySetupManager partySetupManager;


    [SerializeField] CharactersSO[] TestCharacters;

    private void Awake()
    {
        charactersList = new();
        CharacterManager.OnCharacterStorageNew += ActiveCharacter_OnCharacterStorageNew;
        CharacterManager.OnCharacterStorageOld += ActiveCharacter_OnCharacterStorageOld;

        PartyMemberContent.PartyMemberClick += PartyMemberContent_PartyMemberClick;

        player = GetComponentInParent<Player>();
    }

    private void PartyMemberContent_PartyMemberClick(CharacterDataStat CharacterDataStat)
    {
        SwitchCharacter(CharacterDataStat);
    }

    private void ActiveCharacter_OnCharacterStorageOld(CharacterStorage CharacterStorage)
    {
        CharacterStorage.PartySetupManager.OnCurrentPartyChanged -= P_OnCurrentPartyChanged;
    }

    private void ActiveCharacter_OnCharacterStorageNew(CharacterStorage CharacterStorage)
    {
        if (CharacterStorage != null)
        {
            partySetupManager = CharacterStorage.PartySetupManager;
            partySetupManager.OnCurrentPartyChanged += P_OnCurrentPartyChanged;
        }
    }

    private void P_OnCurrentPartyChanged(object sender, System.EventArgs e)
    {
    }

    // Start is called before the first frame update
    private void Start()
    {
        player.PlayerController.playerInputAction.SwitchCharacters.performed += SwitchCharacters_performed;
        TestExistingCharacters();
    }


    private void TestExistingCharacters()
    {
        for (int i = 0; i < TestCharacters.Length; i++)
        {
            PlayableCharacters dc = Instantiate(TestCharacters[i].CharacterPrefab, transform).GetComponent<PlayableCharacters>();

            if (dc == null)
                continue;

            dc.gameObject.SetActive(false);
            partySetupManager.characterStorage.AddCharacterData(dc.GetCharacterDataStat());
            partySetupManager.AddMember(dc.GetCharacterDataStat(), 0);
            charactersList.Add(dc.CharacterSO, dc);
        }

        SwitchCharacter(partySetupManager.GetCurrentPartyMembers()[0], false);
    }


    private void SwitchCharacters_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        int.TryParse(obj.control.name, out int numKeyValue);

        if (numKeyValue > MAX_EQUIP_CHARACTERS || partySetupManager == null)
            return;

        int actualValue = numKeyValue - 1;

        SwitchCharacter(actualValue);
    }

    public bool CanSwitchCharacter(PlayableCharacters pc)
    {
        if (pc == null)
            return false;

        return !pc.PlayableCharacterStateMachine.IsSkillCasting() && !pc.PlayableCharacterStateMachine.playerStateMachine.IsInState<PlayerAirborneState>();
    }

    private void SwitchCharacter(int index, bool playSwitchSound = true)
    {
        if (partySetupManager == null)
            return;

        List<CharacterDataStat> list = partySetupManager.GetCurrentPartyMembers();

        if (index >= list.Count)
            return;

        SwitchCharacter(list[index], playSwitchSound);
    }

    private PlayableCharacters GetPlayableCharacter(CharacterDataStat CharacterDataStat)
    {
        if (CharacterDataStat == null || CharacterDataStat.damageableEntitySO == null ||
            !charactersList.ContainsKey(CharacterDataStat.damageableEntitySO))
            return null;

        return charactersList[CharacterDataStat.damageableEntitySO];
    }
    private void SwitchCharacter(CharacterDataStat PlayableCharacterDataStat, bool playSwitchSound = true)
    {
        if (PlayableCharacterDataStat == null || currentPlayableCharacterData == PlayableCharacterDataStat)
            return;

        PlayableCharacters currentPlayableCharacter = GetPlayableCharacter(currentPlayableCharacterData);

        if (currentPlayableCharacterData != null)
        {
            if (!CanSwitchCharacter(currentPlayableCharacter))
                return;

            currentPlayableCharacter.gameObject.SetActive(false);
            OnPlayerCharacterExit?.Invoke(currentPlayableCharacterData, currentPlayableCharacter);
        }

        currentPlayableCharacterData = PlayableCharacterDataStat;
        currentPlayableCharacter = GetPlayableCharacter(currentPlayableCharacterData);
        currentPlayableCharacter.gameObject.SetActive(true);
        OnPlayerCharacterSwitch?.Invoke(currentPlayableCharacterData, currentPlayableCharacter);

        if (playSwitchSound)
        {
            player.PlayPlayerSoundEffect(player.PlayerSO.SoundData.SwitchClip);
        }
    }

    private void OnDestroy()
    {
        CharacterManager.OnCharacterStorageNew -= ActiveCharacter_OnCharacterStorageNew;
        PartyMemberContent.PartyMemberClick -= PartyMemberContent_PartyMemberClick;
        CharacterManager.OnCharacterStorageOld -= ActiveCharacter_OnCharacterStorageOld;
        if (partySetupManager != null)
        {
            partySetupManager.OnCurrentPartyChanged -= P_OnCurrentPartyChanged;
        }

        player.PlayerController.playerInputAction.SwitchCharacters.performed -= SwitchCharacters_performed;
    }

    // Update is called once per frame
    private void Update()
    {
    }
}
