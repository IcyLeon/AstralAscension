
using System;
using UnityEngine;

public class PartyMember
{
    public CharacterDataStat characterDataStat { get; }

    public PartyMember(CharacterDataStat CharacterDataStat)
    {
        characterDataStat = CharacterDataStat;
    }
}
