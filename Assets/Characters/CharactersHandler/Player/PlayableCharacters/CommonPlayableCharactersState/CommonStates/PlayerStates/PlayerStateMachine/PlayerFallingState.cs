using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerAirborneState
{

    public PlayerFallingState(PlayerStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(playerStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.fallParameter);
        playerStateMachine.playerData.SpeedModifier = 0f;
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
        StopAnimation(playerStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.fallParameter);
    }
}
