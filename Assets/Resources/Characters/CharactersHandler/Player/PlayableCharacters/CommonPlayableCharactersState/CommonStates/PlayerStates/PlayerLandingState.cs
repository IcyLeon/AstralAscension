using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandingState : PlayerGroundedState
{
    public PlayerLandingState(PlayableCharacterStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(playableCharacterStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.landParameter);
        playableCharacterStateMachine.playerData.currentJumpForceMagnitudeXZ = playableCharacterStateMachine.playerData.airborneData.PlayerJumpData.IdleJumpForceMagnitudeXZ;
        playableCharacterStateMachine.ResetVelocity();
    }

    public override void Update()
    {
        base.Update();

        if (!IsMovementKeyPressed())
        {
            return;
        }

        OnMove();
    }

    public override void OnAnimationTransition()
    {
        base.OnAnimationTransition();

        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.playerIdleState);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(playableCharacterStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.landParameter);
    }
}
