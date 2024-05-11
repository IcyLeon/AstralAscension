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
        playerStateMachine.playerData.SpeedModifier = 0f;

    }

    protected override void OnDeadUpdate()
    {
        if (playableCharacters.IsDead())
            return;

        if (playerStateMachine.playerData.movementInput == Vector2.zero)
        {
            playerStateMachine.ChangeState(playerStateMachine.playerIdleState);
            return;
        }
        playerStateMachine.ChangeState(playerStateMachine.playerRunState);
    }
}
