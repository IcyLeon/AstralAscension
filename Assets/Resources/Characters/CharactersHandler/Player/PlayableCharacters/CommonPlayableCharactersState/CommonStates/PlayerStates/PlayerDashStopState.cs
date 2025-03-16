using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashStopState : PlayerStopState
{
    public PlayerDashStopState(PlayableCharacterStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SetAnimationTrigger(playableCharacterStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.dashStopParameter);
        playableCharacterStateMachine.player.playerData.DecelerateForce = playableCharacterStateMachine.player.playerData.groundedData.PlayerStopData.StrongDecelerationForce;
    }
}
