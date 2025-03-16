using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeakStopState : PlayerStopState
{
    public PlayerWeakStopState(PlayerStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SetAnimationTrigger(playerStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.weakStopParameter);
        playerStateMachine.player.playerData.DecelerateForce = playerStateMachine.player.playerData.groundedData.PlayerStopData.WeakDecelerationForce;
    }
}
