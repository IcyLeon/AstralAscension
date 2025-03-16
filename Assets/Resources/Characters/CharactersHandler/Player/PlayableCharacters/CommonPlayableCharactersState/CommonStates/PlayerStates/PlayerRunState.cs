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
        playableCharacterStateMachine.player.playerData.currentJumpForceMagnitudeXZ = playableCharacterStateMachine.player.playerData.airborneData.PlayerJumpData.WeakJumpForceMagnitudeXZ;
        playableCharacterStateMachine.player.playerData.SpeedModifier = playableCharacterStateMachine.player.playerData.groundedData.PlayerRunData.SpeedModifier;
    }

    protected override void OnStop()
    {
        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.playerWeakStopState);
    }
}
