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
        playableCharacterStateMachine.playerData.speedModifier = playableCharacterStateMachine.playerData.groundedData.PlayerSprintData.SpeedModifier;
        playableCharacterStateMachine.playerData.currentJumpForceMagnitudeXZ = playableCharacterStateMachine.playerData.airborneData.PlayerJumpData.StrongJumpForceMagnitudeXZ;
    }

    protected override void OnStop()
    {
        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.playerStrongStopState);
    }

    public override void Exit()
    {
        base.Exit();
        playableCharacterStateMachine.playerData.canSprint = false;
    }
}
