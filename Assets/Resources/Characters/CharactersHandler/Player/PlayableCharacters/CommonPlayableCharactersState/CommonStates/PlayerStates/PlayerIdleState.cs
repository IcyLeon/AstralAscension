using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayableCharacterStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(playableCharacterStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.idleParameter);
        playableCharacterStateMachine.playerData.currentJumpForceMagnitudeXZ = playableCharacterStateMachine.playerData.airborneData.PlayerJumpData.IdleJumpForceMagnitudeXZ;
        playableCharacterStateMachine.ResetVelocity();
    }

    public override void Update()
    {
        base.Update();

        if (!playableCharacterStateMachine.playerData.IsMovementKeyPressed())
        {
            return;
        }

        OnMove();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(playableCharacterStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.idleParameter);
    }
}
