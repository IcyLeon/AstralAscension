using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPlungeState : PlayerAirborneState
{
    public PlayerPlungeState(PlayerStateMachine PS) : base(PS)
    {
    }

    public static Action<Vector3> OnPlungeAction = delegate { };

    public override void Enter()
    {
        base.Enter();
        StartAnimation(playerStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.plungeParameter);
        playerStateMachine.playerData.SpeedModifier = 0f;
        playerStateMachine.player.Rb.useGravity = false;
        playerStateMachine.playerData.allowPlunge = false;
        ResetVelocity();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        LimitFallVelocity();
    }

    public override void Update()
    {
        base.Update();

        if (playerStateMachine.player.Rb.useGravity)
        {
            playerStateMachine.player.Rb.AddForce(
                playerStateMachine.playerData.airborneData.PlayerPlungeData.PlungeSpeed * Vector3.down,
                ForceMode.Acceleration);
        }

        if (IsGrounded())
        {
            OnPlungeAction?.Invoke(playableCharacters.player.Rb.transform.position);

            playerStateMachine.ChangeState(playerStateMachine.playerPlungeLandingState);
            return;
        }
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
        playerStateMachine.playerData.allowPlunge = true;
    }
}
