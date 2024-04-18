using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerGroundedState
{
    public PlayerAttackState(PlayerStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation("isAttacking");
    }

    public override void Update()
    {
        base.Update();
        
        if (!playableCharacters.Animator.GetBool("isAttacking"))
        {
            if (playerStateMachine.playerData.movementInput == Vector2.zero)
            {
                playerStateMachine.ChangeState(playerStateMachine.playerIdleState);
                return;
            }

            OnMove();
        }
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation("isAttacking");
    }
}
