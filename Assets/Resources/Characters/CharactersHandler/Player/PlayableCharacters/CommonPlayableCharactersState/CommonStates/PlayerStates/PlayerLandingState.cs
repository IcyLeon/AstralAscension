using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandingState : PlayerGroundedState
{
    public PlayerLandingState(PlayerStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(playerStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.landParameter);
        playerStateMachine.playerData.currentJumpForceMagnitudeXZ = playerStateMachine.playerData.airborneData.PlayerJumpData.IdleJumpForceMagnitudeXZ;
        playerStateMachine.ResetVelocity();
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

        playerStateMachine.ChangeState(playerStateMachine.playerIdleState);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(playerStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.landParameter);
    }
}
