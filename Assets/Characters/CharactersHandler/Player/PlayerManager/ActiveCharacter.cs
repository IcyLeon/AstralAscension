using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class ActiveCharacter : MonoBehaviour
{
    public delegate void OnPlayerCharacterEvent(CharacterDataStat playerData, PlayableCharacters playableCharacters);
    public static event OnPlayerCharacterEvent OnPlayerCharacterExit;
    public static event OnPlayerCharacterEvent OnPlayerCharacterSwitch;

    private Player player;

    public CharacterDataStat currentPlayableCharacterData { get; private set; }

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        OnPlayerCharacterExit += ActiveCharacter_OnPlayerCharacterExit;
        OnPlayerCharacterSwitch += ActiveCharacter_OnPlayerCharacterSwitch;
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

        PlayableCharacters existPlayableCharacters = GetComponentInChildren<PlayableCharacters>();
        if (existPlayableCharacters != null)
        {
            SwitchCharacter(existPlayableCharacters.GetCharacterDataStat(), false);
        }
    }

    // to set current character active
    private void ActiveCharacter_OnPlayerCharacterSwitch(CharacterDataStat playerData, PlayableCharacters selectedPlayableCharacters)
    {
        if (selectedPlayableCharacters == null)
            return;

        selectedPlayableCharacters.gameObject.SetActive(true);
    }

    private void ActiveCharacter_OnPlayerCharacterExit(CharacterDataStat playerData, PlayableCharacters selectedPlayableCharacters)
    {
        if (selectedPlayableCharacters == null)
            return;

        selectedPlayableCharacters.gameObject.SetActive(false);
    }

    private void SwitchCharacters_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        int numKeyValue;
        int.TryParse(obj.control.name, out numKeyValue);

        //SwitchCharacter(null);

        PlayableCharacters[] list = GetAllPlayableCharacters();
        int actualValue = numKeyValue - 1;

        if (actualValue < list.Length)
        {
            SwitchCharacter(list[actualValue].characterDataStat);
        }
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

    private void SwitchCharacter(CharacterDataStat PlayableCharacterDataStat, bool playSwitchSound = true)
    {
        if (currentPlayableCharacterData == PlayableCharacterDataStat)
            return;

        PlayableCharacters currentPlayableCharacter = GetPlayableCharacters(currentPlayableCharacterData);

        if (currentPlayableCharacterData != null)
        {
            if (!CanSwitchCharacter(currentPlayableCharacter))
                return;

            OnPlayerCharacterExit?.Invoke(currentPlayableCharacterData, currentPlayableCharacter);
        }

        currentPlayableCharacterData = PlayableCharacterDataStat;
        currentPlayableCharacter = GetPlayableCharacters(currentPlayableCharacterData);

        if (currentPlayableCharacterData != null)
        {
            OnPlayerCharacterSwitch?.Invoke(currentPlayableCharacterData, currentPlayableCharacter);

            if (playSwitchSound)
                player.PlayPlayerSoundEffect(player.PlayerSO.SoundData.SwitchClip);
        }
    }

    private void OnDestroy()
    {
        OnPlayerCharacterExit -= ActiveCharacter_OnPlayerCharacterExit;
        OnPlayerCharacterSwitch -= ActiveCharacter_OnPlayerCharacterSwitch;
        player.PlayerController.playerInputAction.SwitchCharacters.performed -= SwitchCharacters_performed;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
