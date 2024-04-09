using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandingState : PlayerGroundedState
{
    public PlayerLandingState(PlayerStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation("isLanding");
        playerStateMachine.playerData.currentJumpForceMagnitudeXZ = playerStateMachine.playerData.airborneData.PlayerJumpData.IdleJumpForceMagnitudeXZ;
        ResetVelocity();
    }

    public override void Update()
    {
        base.Update();

        OnSkillCast();
        if (playerStateMachine.playerData.movementInput == Vector2.zero)
        {
            return;
        }

        OnMove();
    }

    public override void OnAnimationTransition()
    {
        base.OnAnimationTransition();

        playerStateMachine.ChangeState(playerStateMachine.playerIdleState);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation("isLanding");
    }
}
