using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlungeLandingState : PlayerLandingState
{
    public PlayerPlungeLandingState(PlayableCharacterStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SetAnimationTrigger(playableCharacterStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.plungeLandingParameter);
    }
}
