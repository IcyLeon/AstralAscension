using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingThrowState : KeqingElementalSkillState
{
    public KeqingThrowState(PlayableCharacterStateMachine pcs) : base(pcs)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SetAnimationTrigger(keqingStateMachine.keqingAnimationSO.throwParameter);
    }

    public override void OnAnimationTransition()
    {
        base.OnAnimationTransition();

        keqingStateMachine.player.CameraManager.ToggleAimCamera(false, 0.25f);
        keqingStateMachine.ChangeState(keqingStateMachine.swordState);
    }
}
