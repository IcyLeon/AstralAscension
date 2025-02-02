using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlayerDashState : PlayerGroundedState
{
    private float StartTime;
    private bool canRotate;
    public PlayerDashState(PlayerStateMachine PS) : base(PS)
    {
        StartTime = Time.time;
    }

    private AudioClip GetRandomDashClip()
    {
        AudioClip[] clips = playerStateMachine.player.PlayerSO.SoundData.DashClips;
        if (clips.Length == 0)
            return null;

        return clips[Random.Range(0, clips.Length)];
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(playerStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.dashParameter);
        
        playableCharacters.PlayVOAudio(playableCharacters.playerCharactersSO.PlayableCharacterVoicelinesSO.GetRandomDashVOClip());
        playableCharacters.player.PlayPlayerSoundEffect(GetRandomDashClip());

        playerStateMachine.playerData.SpeedModifier = 0f;
        playerStateMachine.playerData.rotationTime = playerStateMachine.playerData.groundedData.PlayerDashData.RotationTime;
        playerStateMachine.playerData.currentJumpForceMagnitudeXZ = playerStateMachine.playerData.airborneData.PlayerJumpData.StrongJumpForceMagnitudeXZ;
        canRotate = IsMovementKeyPressed();
        Dash();
        StartTime = Time.time;
    }

    protected override void Dash_started()
    {
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (canRotate)
        {
            playerStateMachine.SmoothRotateToTargetRotation();
        }

        //playerStateMachine.SmoothRotateToTargetRotation();
    }

    private void UpdateConsecutiveDash()
    {
        if (Time.time - StartTime > playerStateMachine.playerData.groundedData.PlayerDashData.TimeToBeConsideredConsecutive)
        {
            playerStateMachine.playerData.consecutiveDashesUsed = 0;
        }

        playerStateMachine.playerData.consecutiveDashesUsed++;

        if (playerStateMachine.playerData.consecutiveDashesUsed == playerStateMachine.playerData.groundedData.PlayerDashData.ConsecutiveDashesLimitAmount)
        {
            playerStateMachine.playerData.consecutiveDashesUsed = 0;

            playerStateMachine.player.DisableInput(playerController.playerInputAction.Dash, 
                playerStateMachine.playerData.groundedData.PlayerDashData.DashLimitReachedCooldown);
        }

    }

    private void Dash()
    {
        Vector3 dir = playerStateMachine.player.transform.forward;
        if (canRotate)
        {
            dir = GetDirectionXZ(playerStateMachine.playerData.targetYawRotation);
        }

        playerStateMachine.player.Rb.velocity = dir * playerStateMachine.playerData.groundedData.PlayerDashData.BaseDashSpeed;
        UpdateConsecutiveDash();
    }

    //public override void Update()
    //{
    //    base.Update();
    //    RotateToMovementInputDirection();
    //    Vector3 dir = playerStateMachine.player.transform.forward;
    //    playerStateMachine.player.Rb.velocity = playerStateMachine.player.Rb.velocity.magnitude * dir;
    //}

    protected override void OnMovementKeyPerformed()
    {
        playerStateMachine.playerData.canSprint = true;
    }

    public override void OnAnimationTransition()
    {
        base.OnAnimationTransition();

        if (!IsMovementKeyPressed())
        {
            playerStateMachine.ChangeState(playerStateMachine.playerDashStopState);
            return;
        }

        OnMove();
    }

    public override void Exit()
    {
        base.Exit();
        InitBaseRotation();
        StopAnimation(playerStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.dashParameter);
    }
}
