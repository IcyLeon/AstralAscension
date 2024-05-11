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
        StartAnimation(playerStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.attackingParameter);
        ResetVelocity();
    }

    protected override void OnAttackUpdate()
    {
        if (playerStateMachine.PlayableCharacterStateMachine.IsAttacking())
            return;

        if (playerStateMachine.playerData.movementInput == Vector2.zero)
        {
            playerStateMachine.ChangeState(playerStateMachine.playerIdleState);
            return;
        }
        OnMove();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(playerStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.attackingParameter);
        OnAttackInterruptState?.Invoke();
    }
}
