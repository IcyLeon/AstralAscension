using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCAttack02State : PlayableCharacterAttackComboState
{
    public PCAttack02State(PlayableCharacterAttackStateMachine PlayableCharacterAttackStateMachine) : base(PlayableCharacterAttackStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SetAnimationTrigger("2");
    }

    public override void Exit()
    {
        base.Exit();
        playableCharacterAttackStateMachine.playableCharacterAttackData.ResetAttackCooldown();
    }

    public override PlayableCharacterAttackComboState GetNextAttackState()
    {
        return null;
    }
}
