using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementUI_Player : ElementUI
{
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        ActiveCharacter.OnPlayerCharacterExit += ActiveCharacter_OnPlayerCharacterExit;
        ActiveCharacter.OnPlayerCharacterSwitch += ActiveCharacter_OnPlayerCharacterSwitch;
    }

    private void ActiveCharacter_OnPlayerCharacterSwitch(CharacterDataStat playerData, PlayableCharacters playableCharacters)
    {
        characterDataStat = playerData;
        Refresh();

        if (characterDataStat != null)
        {
            characterDataStat.OnElementEnter += OnElementEnter;
            characterDataStat.OnElementExit += OnElementExit;
        }

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

    private void ActiveCharacter_OnPlayerCharacterExit(CharacterDataStat playerData, PlayableCharacters playableCharacters)
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

        ActiveCharacter.OnPlayerCharacterExit -= ActiveCharacter_OnPlayerCharacterExit;
        ActiveCharacter.OnPlayerCharacterSwitch -= ActiveCharacter_OnPlayerCharacterSwitch;
    }
}
