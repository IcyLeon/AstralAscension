using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlayerDashState : PlayerGroundedState
{
    private float startTime;
    private bool canRotate;
    public PlayerDashState(PlayableCharacterStateMachine PS) : base(PS)
    {
        startTime = Time.time;
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

        playableCharacterStateMachine.playerData.SpeedModifier = 0f;
        playableCharacterStateMachine.playerData.rotationTime = playableCharacterStateMachine.playerData.groundedData.PlayerDashData.RotationTime;
        playableCharacterStateMachine.playerData.currentJumpForceMagnitudeXZ = playableCharacterStateMachine.playerData.airborneData.PlayerJumpData.StrongJumpForceMagnitudeXZ;
        canRotate = IsMovementKeyPressed();
        Dash();
        startTime = Time.time;
    }

    protected override void Dash_started()
    {
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (canRotate)
        {
            playableCharacterStateMachine.playerData.SmoothRotateToTargetRotation();
        }

        //playableCharacterStateMachine.SmoothRotateToTargetRotation();
    }

    private void UpdateConsecutiveDash()
    {
        if (Time.time - startTime > playableCharacterStateMachine.playerData.groundedData.PlayerDashData.TimeToBeConsideredConsecutive)
        {
            playableCharacterStateMachine.playerData.consecutiveDashesUsed = 0;
        }

        playableCharacterStateMachine.playerData.consecutiveDashesUsed++;

        if (playableCharacterStateMachine.playerData.consecutiveDashesUsed == playableCharacterStateMachine.playerData.groundedData.PlayerDashData.ConsecutiveDashesLimitAmount)
        {
            playableCharacterStateMachine.playerData.consecutiveDashesUsed = 0;

            playableCharacterStateMachine.player.DisableInput(playableCharacterStateMachine.player.playerController.playerInputAction.Dash, 
                playableCharacterStateMachine.playerData.groundedData.PlayerDashData.DashLimitReachedCooldown);
        }

    }

    private void Dash()
    {
        Vector3 dir = playableCharacterStateMachine.player.transform.forward;
        if (canRotate)
        {
            dir = GetDirectionXZ(playableCharacterStateMachine.playerData.targetYawRotation);
        }

        playableCharacterStateMachine.player.Rb.velocity = dir * playableCharacterStateMachine.playerData.groundedData.PlayerDashData.BaseDashSpeed;
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
        playableCharacterStateMachine.playerData.canSprint = true;
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
