using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterSwitchEvent
{
    public delegate void OnPlayerCharacterEvent(PartyMember NewPartyMember);
    public static event OnPlayerCharacterEvent OnCharacterSwitch;

    public static void Switch(PartyMember NewPartyMember)
    {
        OnCharacterSwitch?.Invoke(NewPartyMember);
    }
}
