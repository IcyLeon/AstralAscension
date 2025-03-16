using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeakStopState : PlayerStopState
{
    public PlayerWeakStopState(PlayableCharacterStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SetAnimationTrigger(playableCharacterStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.weakStopParameter);
        playableCharacterStateMachine.player.playerData.DecelerateForce = playableCharacterStateMachine.player.playerData.groundedData.PlayerStopData.WeakDecelerationForce;
    }
}
