using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingAimState : KeqingElementalSkillState
{
    public KeqingAimState(PlayableCharacterStateMachine pcs) : base(pcs)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StartAnimation(keqingStateMachine.keqingAnimationSO.aimParameter);

        playableCharacterStateMachine.playerStateMachine.ChangeState(
            playableCharacterStateMachine.playerStateMachine.playerAimState
            );

    }
    protected override void SubscribeInputs()
    {
        base.SubscribeInputs();
        keqingStateMachine.player.playerInputAction.ElementalSkill.canceled += ElementalSkill_canceled;
    }

    private void ElementalSkill_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        keqingStateMachine.ChangeState(keqingStateMachine.keqingThrowState);
    }

    protected override void UnsubscribeInputs()
    {
        base.UnsubscribeInputs();
        playableCharacterStateMachine.player.playerInputAction.ElementalSkill.canceled -= ElementalSkill_canceled;

    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(keqingStateMachine.keqingAnimationSO.aimParameter);
    }
}
