using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerMovingState
{
    public PlayerRunState(PlayerStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        playerStateMachine.player.playerData.currentJumpForceMagnitudeXZ = playerStateMachine.player.playerData.airborneData.PlayerJumpData.WeakJumpForceMagnitudeXZ;
        playerStateMachine.player.playerData.SpeedModifier = playerStateMachine.player.playerData.groundedData.PlayerRunData.SpeedModifier;
    }

    protected override void OnStop()
    {
        playerStateMachine.ChangeState(playerStateMachine.playerWeakStopState);
    }
}
