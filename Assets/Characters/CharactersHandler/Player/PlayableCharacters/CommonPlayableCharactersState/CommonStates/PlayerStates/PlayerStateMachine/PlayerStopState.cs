using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStopState : PlayerGroundedState
{
    public PlayerStopState(PlayerStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation("isStopping");
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        playerStateMachine.SmoothRotateToTargetRotation();

        if (IsMovingHorizontal())
        {
            DecelerateHorizontal();
        }
    }

    public override void Update()
    {
        base.Update();

        OnSkillCast();

        if (playerStateMachine.playerData.movementInput == Vector2.zero)
        {
            return;
        }
        OnMove();
    }

    public override void OnAnimationTransition()
    {
        base.OnAnimationTransition();

        playerStateMachine.ChangeState(playerStateMachine.playerIdleState);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation("isStopping");
    }
}
