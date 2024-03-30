using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintState : PlayerMovingState
{
    public PlayerSprintState(PlayerStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        playerStateMachine.playerData.currentJumpForceMagnitudeXZ = playerStateMachine.playerData.airborneData.PlayerJumpData.StrongJumpForceMagnitudeXZ;
        playerStateMachine.playerData.SpeedModifier = playerStateMachine.playerData.groundedData.PlayerSprintData.SpeedModifier;
    }

    protected override void OnStop()
    {
        playerStateMachine.ChangeState(playerStateMachine.playerStrongStopState);
    }

    public override void Exit()
    {
        base.Exit();
        playerStateMachine.playerData.canSprint = false;
    }
}
