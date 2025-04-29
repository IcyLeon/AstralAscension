using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementUI_Player : ElementUI
{
    private Player player;
    private CombatUIManager combatUIManager;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        combatUIManager = GetComponentInParent<CombatUIManager>();
        combatUIManager.OnPlayerChanged += CombatUIManager_OnPlayerChanged;
    }

    private void CombatUIManager_OnPlayerChanged()
    {
        UnsubscribePlayerEvent();
        player = combatUIManager.player;
        SubscribePlayerEvent();
    }

    private void SubscribePlayerEvent()
    {
        if (player == null)
            return;

        player.activeCharacter.OnPlayerCharacterSwitch += ActiveCharacter_OnPlayerCharacterSwitch;
    }

    private void UnsubscribePlayerEvent()
    {
        if (player == null)
            return;

        player.activeCharacter.OnPlayerCharacterSwitch -= ActiveCharacter_OnPlayerCharacterSwitch;
    }

    private void ActiveCharacter_OnPlayerCharacterSwitch(PartyMember PrevPartyMember, PartyMember NewPartyMember)
    {
        UnsubscribeEvent();
        SetCharacterDataStat(NewPartyMember.characterDataStat);
        Refresh();
        SubscribeEvent();
    }

    private void SubscribeEvent()
    {
        if (characterDataStat == null)
            return;

        characterDataStat.OnElementEnter += OnElementEnter;
        characterDataStat.OnElementExit += OnElementExit;
    }

    private void UnsubscribeEvent()
    {
        if (characterDataStat == null)
            return;

        characterDataStat.OnElementEnter -= OnElementEnter;
        characterDataStat.OnElementExit -= OnElementExit;
    }


    private void Refresh()
    {
        if (characterDataStat == null)
            return;

        EID_Dict.Clear();

        if (ObjectPool != null)
            ObjectPool.ResetAll();

        foreach (var elementinfo in characterDataStat.inflictElementList)
        {
            OnElementEnter(elementinfo.Value);
        }
    }


    protected override void OnDestroy()
    {
        base.OnDestroy();
        combatUIManager.OnPlayerChanged -= CombatUIManager_OnPlayerChanged;
        UnsubscribeEvent();
    }
}
