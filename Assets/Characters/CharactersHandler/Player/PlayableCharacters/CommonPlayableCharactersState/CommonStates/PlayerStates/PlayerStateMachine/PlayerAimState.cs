using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimState : PlayerGroundedState
{
    public PlayerAimState(PlayerStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        playerStateMachine.player.playerInputAction.Jump.Disable();
    }

    public override void Exit()
    {
        base.Exit();

        playerStateMachine.player.playerInputAction.Jump.Enable();
    }
}
