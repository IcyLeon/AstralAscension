using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar_Player : Healthbar
{
    // Start is called before the first frame update
    private void Awake()
    {
        ActiveCharacter.OnPlayerCharacterExit += ActiveCharacter_OnPlayerCharacterExit;
        ActiveCharacter.OnPlayerCharacterSwitch += ActiveCharacter_OnPlayerCharacterSwitch;
    }

    private void ActiveCharacter_OnPlayerCharacterExit(CharacterDataStat playerData, PlayableCharacters playableCharacters)
    {
    }

    private void ActiveCharacter_OnPlayerCharacterSwitch(CharacterDataStat playerData, PlayableCharacters playableCharacters)
    {
        SetCharacterDataStat(playerData);
    }

    private void OnDestroy()
    {
        ActiveCharacter.OnPlayerCharacterExit -= ActiveCharacter_OnPlayerCharacterExit;
        ActiveCharacter.OnPlayerCharacterSwitch -= ActiveCharacter_OnPlayerCharacterSwitch;
    }
}
