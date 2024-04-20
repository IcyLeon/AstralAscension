using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGroundedState : PlayerMovementState
{
    public PlayerGroundedState(PlayerStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation("isGrounded");
    }

    public override void SubscribeInputs()
    {
        base.SubscribeInputs();
        playerStateMachine.player.PlayerController.playerInputAction.Jump.started += Jump_started;
        playerStateMachine.player.PlayerController.playerInputAction.Attack.performed += Attack_performed;
        playerStateMachine.player.PlayerController.playerInputAction.Dash.started += Dash_started;
    }

    private void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (this is PlayerAttackState)
            return;

        playerStateMachine.ChangeState(playerStateMachine.playerAttackState);
    }

    protected void OnSkillCast()
    {
        if (!playableCharacters.PlayableCharacterStateMachine.IsSkillCasting())
            return;

        playerStateMachine.ChangeState(playerStateMachine.playerIdleState);
    }

    private void Dash_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (playerStateMachine.IsInState<PlayerDashState>() || IsSkillCasting())
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

        if (!IsGrounded())
        {
            OnFall();
            return;
        }
    }

    public override void UnsubscribeInputs()
    {
        base.UnsubscribeInputs();
        playerStateMachine.player.PlayerController.playerInputAction.Jump.started -= Jump_started;
        playerStateMachine.player.PlayerController.playerInputAction.Dash.started -= Dash_started;
        playerStateMachine.player.PlayerController.playerInputAction.Attack.performed -= Attack_performed;
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
