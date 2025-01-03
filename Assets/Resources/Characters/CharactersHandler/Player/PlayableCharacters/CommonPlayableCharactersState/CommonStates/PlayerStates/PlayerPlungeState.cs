using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlungeState : PlayerAirborneState
{
    public PlayerPlungeState(PlayerStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        playerStateMachine.ResetVelocity();
        StartAnimation(playerStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.plungeParameter);
        playerStateMachine.playerData.SpeedModifier = 0f;
        playerStateMachine.player.Rb.useGravity = false;
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        LimitFallVelocity();
    }

    protected override void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
    }

    public override void Update()
    {
        base.Update();

        if (!playerStateMachine.player.Rb.useGravity)
            return;

        if (IsGrounded())
        {
            playerStateMachine.ChangeState(playerStateMachine.playerPlungeLandingState);
            return;
        }

        playerStateMachine.player.Rb.AddForce(
                (playerStateMachine.playerData.airborneData.PlayerPlungeData.PlungeSpeed * Vector3.down) - GetVerticalVelocity(),
                ForceMode.VelocityChange);
    }

    public override void OnAnimationTransition()
    {
        base.OnAnimationTransition();
        playerStateMachine.player.Rb.useGravity = true;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(playerStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.plungeParameter);
    }
}
