using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar_Player : Healthbar
{
    private Player player;
    private CombatUIManager combatUIManager;

    // Start is called before the first frame update
    private void Awake()
    {
        combatUIManager = GetComponentInParent<CombatUIManager>();
        combatUIManager.OnPlayerChanged += CombatUIManager_OnPlayerChanged;
    }

    private void CombatUIManager_OnPlayerChanged()
    {
        UnsubscribeEvent();
        player = combatUIManager.player;
        SubscribeEvent();
    }

    private void SubscribeEvent()
    {
        if (player == null)
            return;

        combatUIManager.player.activeCharacter.OnPlayerCharacterSwitch += ActiveCharacter_OnPlayerCharacterSwitch;
    }

    private void UnsubscribeEvent()
    {
        if (player == null)
            return;

        combatUIManager.player.activeCharacter.OnPlayerCharacterSwitch -= ActiveCharacter_OnPlayerCharacterSwitch;
    }

    private void ActiveCharacter_OnPlayerCharacterSwitch(PartyMember PrevPartyMember, PartyMember NewPartyMember)
    {
        SetCharacterDataStat(NewPartyMember.characterDataStat);
    }

    private void OnDestroy()
    {
        UnsubscribeEvent();
        combatUIManager.OnPlayerChanged -= CombatUIManager_OnPlayerChanged;
    }
}
