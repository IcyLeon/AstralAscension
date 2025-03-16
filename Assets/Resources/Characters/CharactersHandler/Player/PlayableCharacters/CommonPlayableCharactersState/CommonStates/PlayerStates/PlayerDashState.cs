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

        playerStateMachine.player.playerData.SpeedModifier = 0f;
        playerStateMachine.player.playerData.rotationTime = playerStateMachine.player.playerData.groundedData.PlayerDashData.RotationTime;
        playerStateMachine.player.playerData.currentJumpForceMagnitudeXZ = playerStateMachine.player.playerData.airborneData.PlayerJumpData.StrongJumpForceMagnitudeXZ;
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
            playerStateMachine.player.playerData.SmoothRotateToTargetRotation();
        }

        //playerStateMachine.SmoothRotateToTargetRotation();
    }

    private void UpdateConsecutiveDash()
    {
        if (Time.time - StartTime > playerStateMachine.player.playerData.groundedData.PlayerDashData.TimeToBeConsideredConsecutive)
        {
            playerStateMachine.player.playerData.consecutiveDashesUsed = 0;
        }

        playerStateMachine.player.playerData.consecutiveDashesUsed++;

        if (playerStateMachine.player.playerData.consecutiveDashesUsed == playerStateMachine.player.playerData.groundedData.PlayerDashData.ConsecutiveDashesLimitAmount)
        {
            playerStateMachine.player.playerData.consecutiveDashesUsed = 0;

            playerStateMachine.player.DisableInput(playerController.playerInputAction.Dash, 
                playerStateMachine.player.playerData.groundedData.PlayerDashData.DashLimitReachedCooldown);
        }

    }

    private void Dash()
    {
        Vector3 dir = playerStateMachine.player.transform.forward;
        if (canRotate)
        {
            dir = GetDirectionXZ(playerStateMachine.player.playerData.targetYawRotation);
        }

        playerStateMachine.player.Rb.velocity = dir * playerStateMachine.player.playerData.groundedData.PlayerDashData.BaseDashSpeed;
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
        playerStateMachine.player.playerData.canSprint = true;
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
