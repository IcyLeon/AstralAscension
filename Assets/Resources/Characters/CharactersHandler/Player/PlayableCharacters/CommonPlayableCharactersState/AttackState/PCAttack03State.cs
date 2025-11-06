using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCAttack03State : PlayableCharacterAttackState
{
    public PCAttack03State(PlayableCharacterAttackStateMachine PlayableCharacterAttackStateMachine, PlayableCharacterStateMachine PS) : base(PlayableCharacterAttackStateMachine, PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SetAnimationTrigger("3");
    }

    protected override float AttackDelayInterval()
    {
        return 0f;
    }


    public override void Update()
    {
        base.Update();
        playableCharacterAttackStateMachine.playableCharacterAttackData.ResetAttackCooldown();
    }
}
