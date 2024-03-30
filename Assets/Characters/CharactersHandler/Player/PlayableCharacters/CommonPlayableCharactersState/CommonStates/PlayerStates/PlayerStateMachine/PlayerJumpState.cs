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
        playableCharacters.PlayVOAudio(playableCharacters.PlayerCharactersSO.PlayableCharacterVoicelinesSO.GetRandomJumpVOClip());

        playerStateMachine.playerData.SpeedModifier = 0f; 

        PlayerJumpData playerJumpData = playerStateMachine.playerData.airborneData.PlayerJumpData;
        playerStateMachine.playerData.DecelerateForce = playerJumpData.JumpDecelerationForce;
        playerStateMachine.playerData.rotationTime = playerJumpData.RotationTime;
        canRotate = playerStateMachine.playerData.movementInput != Vector2.zero;

        Jump();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (canRotate)
        {
            SmoothRotateToTargetRotation();
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
            forcedir = GetDirection(playerStateMachine.playerData.targetYawRotation);
        }
        ResetVelocity();

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
