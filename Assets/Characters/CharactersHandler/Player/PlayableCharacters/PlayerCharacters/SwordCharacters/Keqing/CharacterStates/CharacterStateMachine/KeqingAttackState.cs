using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingAttackState : KeqingState
{
    public KeqingAttackState(CharacterStateMachine CharacterStateMachine) : base(CharacterStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        playableCharacterStateMachine.playerStateMachine.playerAttackState.OnAttackStateChange += OnAttackStateChange;
        playableCharacterStateMachine.playerStateMachine.ChangeState(playableCharacterStateMachine.
                                                                    playerStateMachine.playerAttackState);

    }

    protected override void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
    }

    private void OnAttackStateChange()
    {
        keqingStateMachine.ChangeState(keqingStateMachine.swordState);
    }

    public override void Exit()
    {
        base.Exit();
        playableCharacterStateMachine.playerStateMachine.playerAttackState.OnAttackStateChange -= OnAttackStateChange;
    }
}
