using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordReuseableData : PlayableCharacterReuseableData
{
    public SwordReuseableData(int TotalAttackPhase, CharacterStateMachine characterStateMachine) : base(TotalAttackPhase, characterStateMachine)
    {
    }
}
