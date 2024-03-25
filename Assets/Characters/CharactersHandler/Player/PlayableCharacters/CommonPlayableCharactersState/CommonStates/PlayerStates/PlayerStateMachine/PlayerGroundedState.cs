using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGroundedState : PlayerMovementState
{
    public PlayerGroundedState(PlayerStateMachine PS) : base(PS)
    {
    }

    protected void OnMove()
    {
        if (playerStateMachine.playerData.movementInput == Vector2.zero)
        {
            return;
        }

        playerStateMachine.ChangeState(playerStateMachine.playerRunState);
    }
}
