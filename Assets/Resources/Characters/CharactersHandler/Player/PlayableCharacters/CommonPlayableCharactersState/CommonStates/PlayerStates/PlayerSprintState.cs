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
        playerStateMachine.player.playerData.currentJumpForceMagnitudeXZ = playerStateMachine.player.playerData.airborneData.PlayerJumpData.StrongJumpForceMagnitudeXZ;
        playerStateMachine.player.playerData.SpeedModifier = playerStateMachine.player.playerData.groundedData.PlayerSprintData.SpeedModifier;
    }

    protected override void OnStop()
    {
        playerStateMachine.ChangeState(playerStateMachine.playerStrongStopState);
    }

    public override void Exit()
    {
        base.Exit();
        playerStateMachine.player.playerData.canSprint = false;
    }
}
