using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovingState : PlayerGroundedState
{
    public PlayerMovingState(PlayerStateMachine PS) : base(PS)
    {
    }

    protected virtual void OnStop()
    {

    }

    public override void Update()
    {
        base.Update();

        if (playerStateMachine.playerData.movementInput == Vector2.zero)
        {
            OnStop();
            return;
        }
    }
}
