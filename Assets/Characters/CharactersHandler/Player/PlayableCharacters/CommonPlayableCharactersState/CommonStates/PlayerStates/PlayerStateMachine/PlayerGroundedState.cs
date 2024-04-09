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

    protected override void SubscribeInputs()
    {
        base.SubscribeInputs();
        playerStateMachine.player.playerInputAction.Jump.started += Jump_started;
        playerStateMachine.player.playerInputAction.Dash.started += Dash_started;
    }
    protected void OnSkillCast()
    {
        if (!playableCharacters.PlayableCharacterStateMachine.IsSkillCasting())
            return;

        playerStateMachine.ChangeState(playerStateMachine.playerIdleState);
    }

    private void Dash_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (this is PlayerDashState || IsSkillCasting())
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

    protected override void UnsubscribeInputs()
    {
        base.UnsubscribeInputs();
        playerStateMachine.player.playerInputAction.Jump.started -= Jump_started;
        playerStateMachine.player.playerInputAction.Dash.started -= Dash_started;
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
