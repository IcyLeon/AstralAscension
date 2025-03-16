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
    public PlayerDashState(PlayableCharacterStateMachine PS) : base(PS)
    {
        StartTime = Time.time;
    }

    private AudioClip GetRandomDashClip()
    {
        AudioClip[] clips = playableCharacterStateMachine.player.PlayerSO.SoundData.DashClips;
        if (clips.Length == 0)
            return null;

        return clips[Random.Range(0, clips.Length)];
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(playableCharacterStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.dashParameter);
        
        playableCharacter.PlayVOAudio(playableCharacter.playerCharactersSO.PlayableCharacterVoicelinesSO.GetRandomDashVOClip());
        playableCharacter.player.PlayPlayerSoundEffect(GetRandomDashClip());

        playableCharacterStateMachine.player.playerData.SpeedModifier = 0f;
        playableCharacterStateMachine.player.playerData.rotationTime = playableCharacterStateMachine.player.playerData.groundedData.PlayerDashData.RotationTime;
        playableCharacterStateMachine.player.playerData.currentJumpForceMagnitudeXZ = playableCharacterStateMachine.player.playerData.airborneData.PlayerJumpData.StrongJumpForceMagnitudeXZ;
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
            playableCharacterStateMachine.player.playerData.SmoothRotateToTargetRotation();
        }

        //playableCharacterStateMachine.SmoothRotateToTargetRotation();
    }

    private void UpdateConsecutiveDash()
    {
        if (Time.time - StartTime > playableCharacterStateMachine.player.playerData.groundedData.PlayerDashData.TimeToBeConsideredConsecutive)
        {
            playableCharacterStateMachine.player.playerData.consecutiveDashesUsed = 0;
        }

        playableCharacterStateMachine.player.playerData.consecutiveDashesUsed++;

        if (playableCharacterStateMachine.player.playerData.consecutiveDashesUsed == playableCharacterStateMachine.player.playerData.groundedData.PlayerDashData.ConsecutiveDashesLimitAmount)
        {
            playableCharacterStateMachine.player.playerData.consecutiveDashesUsed = 0;

            playableCharacterStateMachine.player.DisableInput(playerController.playerInputAction.Dash, 
                playableCharacterStateMachine.player.playerData.groundedData.PlayerDashData.DashLimitReachedCooldown);
        }

    }

    private void Dash()
    {
        Vector3 dir = playableCharacterStateMachine.player.transform.forward;
        if (canRotate)
        {
            dir = GetDirectionXZ(playableCharacterStateMachine.player.playerData.targetYawRotation);
        }

        playableCharacterStateMachine.player.Rb.velocity = dir * playableCharacterStateMachine.player.playerData.groundedData.PlayerDashData.BaseDashSpeed;
        UpdateConsecutiveDash();
    }

    //public override void Update()
    //{
    //    base.Update();
    //    RotateToMovementInputDirection();
    //    Vector3 dir = playableCharacterStateMachine.player.transform.forward;
    //    playableCharacterStateMachine.player.Rb.velocity = playableCharacterStateMachine.player.Rb.velocity.magnitude * dir;
    //}

    protected override void OnMovementKeyPerformed()
    {
        playableCharacterStateMachine.player.playerData.canSprint = true;
    }

    public override void OnAnimationTransition()
    {
        base.OnAnimationTransition();

        if (!IsMovementKeyPressed())
        {
            playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.playerDashStopState);
            return;
        }

        OnMove();
    }

    public override void Exit()
    {
        base.Exit();
        InitBaseRotation();
        StopAnimation(playableCharacterStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.dashParameter);
    }
}
