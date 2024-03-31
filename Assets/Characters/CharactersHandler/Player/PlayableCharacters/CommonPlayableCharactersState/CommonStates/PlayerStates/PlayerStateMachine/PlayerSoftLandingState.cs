using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoftLandingState : PlayerLandingState
{
    public PlayerSoftLandingState(PlayerStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(playerStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.weakLandParameter);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(playerStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.weakLandParameter);
    }
}
