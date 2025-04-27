using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerMovingState
{
    public PlayerRunState(PlayableCharacterStateMachine PS) : base(PS)
    {
        playableCharacterStateMachine.playerData.speedModifier = playableCharacterStateMachine.playerData.groundedData.PlayerRunData.SpeedModifier;
    }

    public override void Enter()
    {
        base.Enter();
        playableCharacterStateMachine.playerData.currentJumpForceMagnitudeXZ = playableCharacterStateMachine.playerData.airborneData.PlayerJumpData.WeakJumpForceMagnitudeXZ;
    }

    protected override void OnStop()
    {
        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.playerWeakStopState);
    }
}
