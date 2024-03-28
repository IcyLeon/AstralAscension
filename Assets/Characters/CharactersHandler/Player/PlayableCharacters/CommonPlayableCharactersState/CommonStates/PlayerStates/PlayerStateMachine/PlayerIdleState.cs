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
        playerStateMachine.playerData.currentJumpForceMagnitudeXZ = playerStateMachine.playerData.airborneData.PlayerJumpData.IdleJumpForceMagnitudeXZ;
        ResetVelocity();
    }

    public override void Update()
    {
        base.Update();

        OnMove();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
