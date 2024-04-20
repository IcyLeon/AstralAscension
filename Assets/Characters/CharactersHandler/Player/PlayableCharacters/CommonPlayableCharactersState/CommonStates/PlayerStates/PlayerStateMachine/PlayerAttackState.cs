using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerGroundedState
{
    private string AttackParameter;
    public delegate void onAttackStateChange();
    public event onAttackStateChange OnAttackStateChange;

    public PlayerAttackState(PlayerStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        AttackParameter = playerStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.attackingParameter;
        StartAnimation(AttackParameter);
        ResetVelocity();
    }

    public override void Update()
    {
        base.Update();
        
        if (!playableCharacters.Animator.GetBool(AttackParameter))
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
        StopAnimation(AttackParameter);
        OnAttackStateChange?.Invoke();
    }
}
