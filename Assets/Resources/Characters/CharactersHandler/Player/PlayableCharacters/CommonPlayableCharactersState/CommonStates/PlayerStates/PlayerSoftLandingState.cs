using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoftLandingState : PlayerLandingState
{
    public PlayerSoftLandingState(PlayableCharacterStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SetAnimationTrigger(playableCharacterStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.weakLandParameter);
    }
}
