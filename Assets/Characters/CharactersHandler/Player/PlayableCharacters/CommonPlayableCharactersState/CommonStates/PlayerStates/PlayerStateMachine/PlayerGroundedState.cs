using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGroundedState : PlayerMovementState
{
    public PlayerGroundedState(PlayerStateMachine PS) : base(PS)
    {
    }

    protected override void SubscribeInputs()
    {
        base.SubscribeInputs();
        playerStateMachine.player.playerInputAction.Jump.started += Jump_started;
    }

    private void Jump_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnJump();
    }

    protected override void UnsubscribeInputs()
    {
        base.UnsubscribeInputs();
        playerStateMachine.player.playerInputAction.Jump.started -= Jump_started;
    }

    protected void OnMove()
    {
        if (playerStateMachine.playerData.movementInput == Vector2.zero)
        {
            return;
        }

        playerStateMachine.ChangeState(playerStateMachine.playerRunState);
    }

    private void OnJump()
    {
        playerStateMachine.ChangeState(playerStateMachine.playerJumpState);
        return;
    }
}
