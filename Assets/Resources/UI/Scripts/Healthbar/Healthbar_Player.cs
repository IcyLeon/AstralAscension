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

    private void ActiveCharacter_OnPlayerCharacterExit(CharacterDataStat playerData, PartyMember PartyMember)
    {
    }

    private void ActiveCharacter_OnPlayerCharacterSwitch(CharacterDataStat playerData, PartyMember PartyMember)
    {
        SetCharacterDataStat(playerData);
    }

    private void OnDestroy()
    {
        ActiveCharacter.OnPlayerCharacterExit -= ActiveCharacter_OnPlayerCharacterExit;
        ActiveCharacter.OnPlayerCharacterSwitch -= ActiveCharacter_OnPlayerCharacterSwitch;
    }
}
