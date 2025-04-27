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
        UnsubscribeEvent();
        player = combatUIManager.player;
        SubscribeEvent();
    }

    private void SubscribeEvent()
    {
        if (player == null)
            return;

        player.activeCharacter.OnPlayerCharacterExit += ActiveCharacter_OnPlayerCharacterExit;
        player.activeCharacter.OnPlayerCharacterSwitch += ActiveCharacter_OnPlayerCharacterSwitch;
    }

    private void UnsubscribeEvent()
    {
        if (player == null)
            return;

        player.activeCharacter.OnPlayerCharacterExit -= ActiveCharacter_OnPlayerCharacterExit;
        player.activeCharacter.OnPlayerCharacterSwitch -= ActiveCharacter_OnPlayerCharacterSwitch;
    }

    private void ActiveCharacter_OnPlayerCharacterSwitch(CharacterDataStat playerData, PartyMember PartyMember)
    {
        SetCharacterDataStat(playerData);
        Refresh();

        if (GetCharacterDataStat() != null)
        {
            GetCharacterDataStat().OnElementEnter += OnElementEnter;
            GetCharacterDataStat().OnElementExit += OnElementExit;
        }

    }

    private void Refresh()
    {
        if (GetCharacterDataStat() == null)
            return;

        EID_Dict.Clear();

        if (ObjectPool != null)
            ObjectPool.ResetAll();

        foreach (var elementinfo in GetCharacterDataStat().inflictElementList)
        {
            OnElementEnter(elementinfo.Value);
        }
    }

    private void ActiveCharacter_OnPlayerCharacterExit(CharacterDataStat playerData, PartyMember PartyMember)
    {
        if (playerData != null)
        {
            playerData.OnElementEnter -= OnElementEnter;
            playerData.OnElementExit -= OnElementExit;
        }


    }


    protected override void OnDestroy()
    {
        base.OnDestroy();
        combatUIManager.OnPlayerChanged -= CombatUIManager_OnPlayerChanged;
        UnsubscribeEvent();
    }
}
