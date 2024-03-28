using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        playerStateMachine.playerData.SpeedModifier = 0f;
        playerStateMachine.playerData.currentJumpForceMagnitudeXZ = playerStateMachine.playerData.airborneData.PlayerJumpData.IdleJumpForceMagnitudeXZ;
        ResetVelocity();
    }

    public override void Update()
    {
        base.Update();

        if (playerStateMachine.playerData.movementInput == Vector2.zero)
        {
            return;
        }
        OnMove();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
