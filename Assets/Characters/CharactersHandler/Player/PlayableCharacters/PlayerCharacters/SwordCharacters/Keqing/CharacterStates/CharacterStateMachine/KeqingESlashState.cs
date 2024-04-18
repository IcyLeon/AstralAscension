using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingESlashState : KeqingElementalSkillState
{
    public KeqingESlashState(PlayableCharacterStateMachine pcs) : base(pcs)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SetAnimationTrigger(keqingStateMachine.keqingAnimationSO.elementalSkillSlashParameter);
    }

    public override void OnAnimationTransition()
    {
        base.OnAnimationTransition();

        keqingStateMachine.ChangeState(keqingStateMachine.swordState);
    }

    public override void Exit()
    {
        base.Exit();
        keqing.player.Rb.useGravity = true;
    }
}
