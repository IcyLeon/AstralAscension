using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashStopState : PlayerStopState
{
    public PlayerDashStopState(PlayerStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SetAnimationTrigger(playerStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.dashStopParameter);
        playerStateMachine.playerData.DecelerateForce = playerStateMachine.playerData.groundedData.PlayerStopData.StrongDecelerationForce;
    }
}
