using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCAttack02State : PlayableCharacterAttackState
{
    public PCAttack02State(PlayableCharacterAttackStateMachine PlayableCharacterAttackStateMachine, PlayableCharacterStateMachine PS) : base(PlayableCharacterAttackStateMachine, PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SetAnimationTrigger("2");
    }

    protected override float AttackDelayInterval()
    {
        return 0.75f;
    }

    protected override void TransitNextAttack()
    {
        playableCharacterStateMachine.ChangeState(new PCAttack03State(playableCharacterAttackStateMachine, playableCharacterStateMachine));
    }
}
