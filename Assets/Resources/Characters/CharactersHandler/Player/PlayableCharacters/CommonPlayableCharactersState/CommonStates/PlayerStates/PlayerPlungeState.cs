using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlungeState : PlayerAirborneState
{
    public PlayerPlungeState(PlayableCharacterStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        playableCharacterStateMachine.ResetVelocity();
        StartAnimation(playableCharacterStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.plungeParameter);
        playableCharacterStateMachine.playerData.SpeedModifier = 0f;
        playableCharacterStateMachine.player.Rb.useGravity = false;
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

        if (!playableCharacterStateMachine.player.Rb.useGravity)
            return;

        if (IsGrounded())
        {
            playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.playerPlungeLandingState);
            return;
        }

        playableCharacterStateMachine.player.Rb.AddForce(
                (playableCharacterStateMachine.playerData.airborneData.PlayerPlungeData.PlungeSpeed * Vector3.down) - GetVerticalVelocity(),
                ForceMode.VelocityChange);
    }

    public override void OnAnimationTransition()
    {
        base.OnAnimationTransition();
        playableCharacterStateMachine.player.Rb.useGravity = true;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(playableCharacterStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.plungeParameter);
    }
}
