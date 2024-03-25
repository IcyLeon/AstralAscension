using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStopState : PlayerGroundedState
{
    public PlayerStopState(PlayerStateMachine PS) : base(PS)
    {
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        RotateTowardsTargetRotation();
    }

    public override void OnAnimationTransition()
    {
        base.OnAnimationTransition();

        if (playerStateMachine.playerData.movementInput == Vector2.zero)
        {
            playerStateMachine.ChangeState(playerStateMachine.playerIdleState);
            return;
        }

        OnMove();
    }
}
