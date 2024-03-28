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

    private void Dash_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (this is PlayerDashState)
        {
            return;
        }

        //playerStateMachine.ChangeState(playerStateMachine.playerDashState);
    }

    private void Jump_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
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
        playerStateMachine.ChangeState(playerStateMachine.playerFallingState);
    }
}
