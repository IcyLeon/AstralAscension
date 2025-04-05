using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStrongStopState : PlayerStopState
{
    public PlayerStrongStopState(PlayableCharacterStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SetAnimationTrigger(playableCharacterStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.strongStopParameter);
        playableCharacterStateMachine.playerData.DecelerateForce = playableCharacterStateMachine.playerData.groundedData.PlayerStopData.StrongDecelerationForce;
    }
}
