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
        playableCharacterStateMachine.playerData.DecelerateForce = playableCharacterStateMachine.playerData.airborneData.PlayerJumpData.JumpDecelerationForce;
        playableCharacterStateMachine.playerData.rotationTime = playableCharacterStateMachine.playerData.airborneData.PlayerJumpData.RotationTime;
        canRotate = !playableCharacterStateMachine.playerData.IsMovementKeyPressed();

        Jump();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (canRotate)
        {
            playableCharacterStateMachine.playerData.SmoothRotateToTargetRotation();
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
            forcedir = GetDirectionXZ(playableCharacterStateMachine.playerData.targetYawRotation);
        }
        playableCharacterStateMachine.ResetVelocity();

        forcedir = forcedir.normalized * playableCharacterStateMachine.playerData.currentJumpForceMagnitudeXZ;
        forcedir.y = playableCharacterStateMachine.playerData.airborneData.PlayerJumpData.JumpForceY;
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
