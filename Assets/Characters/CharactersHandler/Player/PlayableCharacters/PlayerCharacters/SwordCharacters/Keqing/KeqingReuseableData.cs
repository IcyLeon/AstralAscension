using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingReuseableData : SwordReuseableData
{
    public Vector3 targetPosition;

    public KeqingReuseableData(int TotalAttackPhase, CharacterStateMachine characterStateMachine) : base(TotalAttackPhase, characterStateMachine)
    {
        targetPosition = Vector3.zero;
    }
}
