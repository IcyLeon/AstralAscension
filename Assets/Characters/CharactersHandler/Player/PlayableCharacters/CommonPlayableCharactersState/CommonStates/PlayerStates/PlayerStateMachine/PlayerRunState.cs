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
        playerStateMachine.playerData.currentJumpForceMagnitudeXZ = playerStateMachine.playerData.airborneData.PlayerJumpData.WeakJumpForceMagnitudeXZ;
        playerStateMachine.playerData.SpeedModifier = playerStateMachine.playerData.groundedData.PlayerRunData.SpeedModifier;
    }

    protected override void OnStop()
    {
        playerStateMachine.ChangeState(playerStateMachine.playerWeakStopState);
    }
}
