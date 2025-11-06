using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementUI_Player : ElementUI
{
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        CharacterSwitchEvent.OnCharacterSwitch += CharacterSwitchEvent_OnCharacterSwitch;
    }

    private void CharacterSwitchEvent_OnCharacterSwitch(PartyMember PartyMember)
    {
        UnsubscribeEvent();
        SetCharacterDataStat(PartyMember.characterDataStat);
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
        UnsubscribeEvent();
        CharacterSwitchEvent.OnCharacterSwitch -= CharacterSwitchEvent_OnCharacterSwitch;
    }
}
