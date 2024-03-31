using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDeadState : PlayerMovementState
{
    public PlayerDeadState(PlayerStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //StartAnimation()
        playerStateMachine.playerData.SpeedModifier = 0f;

    }

    public override void Update()
    {
        base.Update();

        if (!playableCharacters.IsDead())
        {
            if (playerStateMachine.playerData.movementInput == Vector2.down)
            {
                playerStateMachine.ChangeState(playerStateMachine.playerIdleState);
                return;
            }
            playerStateMachine.ChangeState(playerStateMachine.playerRunState);
        }
    }

    public override void Exit()
    {
        base.Exit();

    }
}
