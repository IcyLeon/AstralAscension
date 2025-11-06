using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCAttack01State : PlayableCharacterAttackState
{
    public PCAttack01State(PlayableCharacterAttackStateMachine PlayableCharacterAttackStateMachine, PlayableCharacterStateMachine PS) : base(PlayableCharacterAttackStateMachine, PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SetAnimationTrigger("1");
    }

    protected override float AttackDelayInterval()
    {
        return 0.35f;
    }

    protected override void TransitNextAttack()
    {
        playableCharacterStateMachine.ChangeState(new PCAttack02State(playableCharacterAttackStateMachine, playableCharacterStateMachine));
    }
}
