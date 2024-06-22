using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PartyMemberContent : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private PartyMemberInfo PartyMemberInfo;
    [SerializeField] private TextMeshProUGUI IndexTxt;
    private CharacterDataStat characterDataStat;
    public delegate void OnPartyInfo(CharacterDataStat characterDataStat);
    public static event OnPartyInfo PartyMemberClick;

    public void SetCharacterDataStat(CharacterDataStat CharacterDataStat)
    {
        characterDataStat = CharacterDataStat;
        PartyMemberInfo.SetCharacterDataStat(characterDataStat);
    }

    public void SetIndexKeyText(int idx)
    {
        IndexTxt.text = idx.ToString();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (characterDataStat == null)
            return;

        PartyMemberClick?.Invoke(characterDataStat);
    }
}
