using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlungeLandingState : PlayerLandingState
{
    public PlayerPlungeLandingState(PlayerStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(playerStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.plungeLandingParameter);
    }

    public override void Update()
    {
        base.Update();

        if (playerStateMachine.playerData.movementInput == Vector2.zero)
        {
            return;
        }

        OnMove();
    }

    public override void OnAnimationTransition()
    {
        base.OnAnimationTransition();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(playerStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.plungeLandingParameter);
    }
}
