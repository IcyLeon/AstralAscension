using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDashState : PlayerGroundedState
{
    private float StartTime;
    private bool canRotate;
    public PlayerDashState(PlayerStateMachine PS) : base(PS)
    {
        StartTime = Time.time;
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(playerStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.dashParameter);
        playerStateMachine.playerData.SpeedModifier = 0f;
        playerStateMachine.playerData.currentJumpForceMagnitudeXZ = playerStateMachine.playerData.airborneData.PlayerJumpData.StrongJumpForceMagnitudeXZ;
        canRotate = playerStateMachine.playerData.movementInput != Vector2.zero;
        Dash();
        StartTime = Time.time;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (canRotate)
        {
            SmoothRotateToTargetRotation();
        }
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

            playerStateMachine.player.DisableInput(playerStateMachine.player.playerInputAction.Dash, 
                playerStateMachine.playerData.groundedData.PlayerDashData.DashLimitReachedCooldown);
        }

    }

    private void Dash()
    {
        Vector3 dir = playerStateMachine.player.transform.forward;
        if (canRotate)
        {
            dir = GetDirection(playerStateMachine.playerData.targetYawRotation);
        }

        playerStateMachine.player.Rb.velocity = dir * playerStateMachine.playerData.groundedData.PlayerDashData.BaseDashSpeed;
        UpdateConsecutiveDash();
    }

    protected override void Movement_performed(Vector2 movementInput)
    {
        playerStateMachine.playerData.canSprint = true;
    }

    public override void OnAnimationTransition()
    {
        base.OnAnimationTransition();

        if (playerStateMachine.playerData.movementInput == Vector2.zero)
        {
            playerStateMachine.ChangeState(playerStateMachine.playerStrongStopState);
            return;
        }

        OnMove();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(playerStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.dashParameter);
    }
}
