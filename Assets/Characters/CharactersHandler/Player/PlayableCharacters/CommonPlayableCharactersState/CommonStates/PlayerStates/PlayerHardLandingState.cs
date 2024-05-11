using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHardLandingState : PlayerLandingState
{
    public PlayerHardLandingState(PlayerStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SetAnimationTrigger(playerStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.hardLandParameter);
    }
}
