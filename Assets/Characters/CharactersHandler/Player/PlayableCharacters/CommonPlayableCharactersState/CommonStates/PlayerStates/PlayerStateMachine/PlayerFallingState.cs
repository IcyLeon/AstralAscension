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

    private float GetSpeedY()
    {
        return Mathf.Abs(GetVerticalVelocity().y);
    }

    public override void Update()
    {
        base.Update();

        if (IsGrounded())
        {
            if (GetSpeedY() < 10f) // got issue
            {
                playerStateMachine.ChangeState(playerStateMachine.playerSoftLandingState);
                return;
            }
            playerStateMachine.ChangeState(playerStateMachine.playerHardLandingState);
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(playerStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.fallParameter);
    }
}
