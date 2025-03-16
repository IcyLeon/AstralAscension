using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAirborneState
{
    private bool canRotate = false;
    private bool canStartFalling;

    public PlayerJumpState(PlayableCharacterStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StartAnimation(playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.jumpParameter);
        playableCharacter.PlayVOAudio(playableCharacter.playerCharactersSO.PlayableCharacterVoicelinesSO.GetRandomJumpVOClip());

        playableCharacterStateMachine.player.playerData.SpeedModifier = 0f; 

        playableCharacterStateMachine.player.playerData.DecelerateForce = playableCharacterStateMachine.player.playerData.airborneData.PlayerJumpData.JumpDecelerationForce;
        playableCharacterStateMachine.player.playerData.rotationTime = playableCharacterStateMachine.player.playerData.airborneData.PlayerJumpData.RotationTime;
        canRotate = !IsMovementKeyPressed();

        Jump();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (canRotate)
        {
            playableCharacterStateMachine.player.playerData.SmoothRotateToTargetRotation();
        }

        if (IsMovingUp())
        {
            DecelerateVertical();
        }
    }

    private void Jump()
    {
        Vector3 forcedir = playableCharacterStateMachine.player.transform.forward;
        if (canRotate)
        {
            forcedir = GetDirectionXZ(playableCharacterStateMachine.player.playerData.targetYawRotation);
        }
        playableCharacterStateMachine.ResetVelocity();

        forcedir = forcedir.normalized * playableCharacterStateMachine.player.playerData.currentJumpForceMagnitudeXZ;
        forcedir.y = playableCharacterStateMachine.player.playerData.airborneData.PlayerJumpData.JumpForceY;
        playableCharacterStateMachine.player.Rb.AddForce(forcedir, ForceMode.VelocityChange);
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
        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.playerFallingState);
    }

    public override void Exit()
    {
        base.Exit();
        InitBaseRotation();
        StopAnimation(playableCharacterStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.jumpParameter);
        canStartFalling = canRotate = false;
    }
}
