using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAirborneState
{
    private bool canRotate = false;
    private bool canStartFalling;

    public PlayerJumpState(PlayerStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StartAnimation(playableCharacters.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.jumpParameter);
        playableCharacters.PlayVOAudio(playableCharacters.playerCharactersSO.PlayableCharacterVoicelinesSO.GetRandomJumpVOClip());

        playerStateMachine.playerData.SpeedModifier = 0f; 

        playerStateMachine.playerData.DecelerateForce = playerStateMachine.playerData.airborneData.PlayerJumpData.JumpDecelerationForce;
        playerStateMachine.playerData.rotationTime = playerStateMachine.playerData.airborneData.PlayerJumpData.RotationTime;
        canRotate = !IsMovementKeyPressed();

        Jump();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (canRotate)
        {
            playerStateMachine.SmoothRotateToTargetRotation();
        }

        if (IsMovingUp())
        {
            DecelerateVertical();
        }
    }

    private void Jump()
    {
        Vector3 forcedir = playerStateMachine.player.transform.forward;
        if (canRotate)
        {
            forcedir = GetDirectionXZ(playerStateMachine.playerData.targetYawRotation);
        }
        playerStateMachine.ResetVelocity();

        forcedir = forcedir.normalized * playerStateMachine.playerData.currentJumpForceMagnitudeXZ;
        forcedir.y = playerStateMachine.playerData.airborneData.PlayerJumpData.JumpForceY;
        playerStateMachine.player.Rb.AddForce(forcedir, ForceMode.VelocityChange);
    }

    public override void Update()
    {
        base.Update();

        if (!canStartFalling && IsMovingUp(0f))
        {
            canStartFalling = true;
        }

        if (!canStartFalling || IsMovingUp(0f))
        {
            return;
        }

        OnFall();
    }

    private void OnFall()
    {
        playerStateMachine.ChangeState(playerStateMachine.playerFallingState);
    }

    public override void Exit()
    {
        base.Exit();
        InitBaseRotation();
        StopAnimation(playerStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.jumpParameter);
        canStartFalling = canRotate = false;
    }
}
