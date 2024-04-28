using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerGroundedState : PlayerMovementState
{
    public PlayerGroundedState(PlayerStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(playerStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.groundParameter);
        playerStateMachine.player.PlayerController.playerInputAction.Jump.started += Jump_started;
        playerStateMachine.player.PlayerController.playerInputAction.Dash.started += Dash_started;
    }

    public override void Exit()
    {
        base.Exit();
        playerStateMachine.player.PlayerController.playerInputAction.Jump.started -= Jump_started;
        playerStateMachine.player.PlayerController.playerInputAction.Dash.started -= Dash_started;
    }

    protected virtual void OnSkillCast()
    {
        if (!IsSkillCasting())
            return;

        playerStateMachine.ChangeState(playerStateMachine.playerIdleState);
    }

    protected virtual void Dash_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (IsSkillCasting())
            return;

        playerStateMachine.ChangeState(playerStateMachine.playerDashState);
    }

    private void Jump_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (IsSkillCasting())
            return;

        OnJump();
    }

    public override void Update()
    {
        base.Update();

        OnSkillCast();
        OnAttackUpdate();

        if (!IsGrounded())
        {
            OnFall();
            return;
        }
    }

    protected virtual void OnAttackUpdate()
    {
        if (!playerStateMachine.PlayableCharacterStateMachine.IsAttacking())
            return;

        playerStateMachine.ChangeState(playerStateMachine.playerIdleState);
    }

    protected void OnMove()
    {
        if (playerStateMachine.playerData.canSprint)
        {
            playerStateMachine.ChangeState(playerStateMachine.playerSprintState);
            return;
        }

        playerStateMachine.ChangeState(playerStateMachine.playerRunState);
    }

    private void OnJump()
    {
        playerStateMachine.ChangeState(playerStateMachine.playerJumpState);
    }

    private void OnFall()
    {
        if (IsSkillCasting())
            return;

        playerStateMachine.ChangeState(playerStateMachine.playerFallingState);
    }
}
