using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintState : PlayerMovingState
{
    public PlayerSprintState(PlayableCharacterStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        playableCharacterStateMachine.player.playerData.currentJumpForceMagnitudeXZ = playableCharacterStateMachine.player.playerData.airborneData.PlayerJumpData.StrongJumpForceMagnitudeXZ;
        playableCharacterStateMachine.player.playerData.SpeedModifier = playableCharacterStateMachine.player.playerData.groundedData.PlayerSprintData.SpeedModifier;
    }

    protected override void OnStop()
    {
        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.playerStrongStopState);
    }

    public override void Exit()
    {
        base.Exit();
        playableCharacterStateMachine.player.playerData.canSprint = false;
    }
}
