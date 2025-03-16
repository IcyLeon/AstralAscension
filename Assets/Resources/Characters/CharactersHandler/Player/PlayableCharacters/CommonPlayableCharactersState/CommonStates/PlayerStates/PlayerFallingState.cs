using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerAirborneState
{

    public PlayerFallingState(PlayableCharacterStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(playableCharacterStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.fallParameter);
        playableCharacterStateMachine.player.playerData.SpeedModifier = 0f;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        LimitFallVelocity();
    }

    public override void Update()
    {
        base.Update();
        OnGroundTransition();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(playableCharacterStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.fallParameter);
    }
}
