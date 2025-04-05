using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCAttack01State : PlayableCharacterAttackComboState
{
    public PCAttack01State(PlayableCharacterAttackStateMachine PlayableCharacterAttackStateMachine) : base(PlayableCharacterAttackStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SetAnimationTrigger("1");
    }

    public override PlayableCharacterAttackComboState GetNextAttackState()
    {
        return playableCharacterAttackStateMachine.PCAttack02State;
    }
}
