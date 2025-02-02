using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerGroundedState
{
    public delegate void OnAttackInterruptStateChange();
    public static OnAttackInterruptStateChange OnAttackInterruptState;

    public PlayerAttackState(PlayerStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(playerStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.attackParameter);
        playerStateMachine.ResetVelocity();
    }

    protected override void OnAttackUpdate()
    {
        if (playerStateMachine.PlayableCharacterStateMachine.IsAttacking())
            return;

        if (!IsMovementKeyPressed())
        {
            playerStateMachine.ChangeState(playerStateMachine.playerIdleState);
            return;
        }
        OnMove();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(playerStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.attackParameter);
        OnAttackInterruptState?.Invoke();
    }
}
