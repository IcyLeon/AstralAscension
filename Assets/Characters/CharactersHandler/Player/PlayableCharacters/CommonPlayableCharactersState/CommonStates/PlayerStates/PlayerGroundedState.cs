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
        StartAnimation(playableCharacters.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.groundParameter);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        playerStateMachine.player.PlayerController.playerInputAction.Jump.started += Jump_started;
        playerStateMachine.player.PlayerController.playerInputAction.Dash.started += Dash_started;
    }
    public override void OnDisable()
    {
        base.OnDisable();
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

        OnAttackUpdate();
        OnSkillCast();

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

        playerStateMachine.ChangeState(playerStateMachine.playerAttackState);
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