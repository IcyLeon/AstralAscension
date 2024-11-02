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
    private void Dash_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (IsSkillCasting())
            return;

        Dash_started();
    }

    private void Jump_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (IsSkillCasting())
            return;

        Jump_started();
    }

    protected virtual void Dash_started()
    {
        playerStateMachine.ChangeState(playerStateMachine.playerDashState);
    }

    protected virtual void Jump_started()
    {
        playerStateMachine.ChangeState(playerStateMachine.playerJumpState);
    }

    protected virtual void OnSkillCastUpdate()
    {
        if (!IsSkillCasting())
            return;

        playerStateMachine.ChangeState(playerStateMachine.playerIdleState);
    }

    public override void Update()
    {
        base.Update();

        OnAttackUpdate();
        OnSkillCastUpdate();

        if (!IsGrounded())
        {
            OnFall();
            return;
        }
    }

    protected virtual void OnAttackUpdate()
    {
        if (IsSkillCasting())
            return;

        if (!playerStateMachine.PlayableCharacterStateMachine.IsAttacking())
            return;

        playerStateMachine.ChangeState(playerStateMachine.playerAttackState);
    }

    protected void OnMove()
    {
        if (IsSkillCasting())
            return;

        if (playerStateMachine.playerData.canSprint)
        {
            playerStateMachine.ChangeState(playerStateMachine.playerSprintState);
            return;
        }

        playerStateMachine.ChangeState(playerStateMachine.playerRunState);
    }

    private void OnFall()
    {
        if (IsSkillCasting())
            return;

        playerStateMachine.ChangeState(playerStateMachine.playerFallingState);
    }
}
