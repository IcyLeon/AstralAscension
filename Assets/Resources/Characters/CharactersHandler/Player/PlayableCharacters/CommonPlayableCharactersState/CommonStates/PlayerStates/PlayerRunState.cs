using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerMovingState
{
    public PlayerRunState(PlayableCharacterStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        playableCharacterStateMachine.playerData.currentJumpForceMagnitudeXZ = playableCharacterStateMachine.playerData.airborneData.PlayerJumpData.WeakJumpForceMagnitudeXZ;
        playableCharacterStateMachine.playerData.SpeedModifier = playableCharacterStateMachine.playerData.groundedData.PlayerRunData.SpeedModifier;
    }

    protected override void OnStop()
    {
        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.playerWeakStopState);
    }
}
