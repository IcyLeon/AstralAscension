using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHardLandingState : PlayerLandingState
{
    public PlayerHardLandingState(PlayableCharacterStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SetAnimationTrigger(playableCharacterStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.hardLandParameter);
    }
}
