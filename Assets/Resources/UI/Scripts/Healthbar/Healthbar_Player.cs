using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar_Player : Healthbar
{
    // Start is called before the first frame update
    private void Awake()
    {
        CharacterSwitchEvent.OnCharacterSwitch += CharacterSwitchEvent_OnCharacterSwitch;
    }

    private void CharacterSwitchEvent_OnCharacterSwitch(PartyMember PartyMember)
    {
        SetCharacterDataStat(PartyMember.characterDataStat);
    }


    private void OnDestroy()
    {
        CharacterSwitchEvent.OnCharacterSwitch -= CharacterSwitchEvent_OnCharacterSwitch;
    }
}
