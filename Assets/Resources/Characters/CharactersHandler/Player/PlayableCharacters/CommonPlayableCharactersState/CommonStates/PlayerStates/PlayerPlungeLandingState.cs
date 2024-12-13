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
        SetAnimationTrigger(playerStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.plungeLandingParameter);
    }
}
