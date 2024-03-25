using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        ResetVelocity();
    }

    public override void Update()
    {
        base.Update();

        OnMove();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
