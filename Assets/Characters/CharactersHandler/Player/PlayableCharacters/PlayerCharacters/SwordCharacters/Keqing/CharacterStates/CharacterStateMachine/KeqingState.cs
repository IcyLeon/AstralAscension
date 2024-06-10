using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeqingState : SwordState
{
    public KeqingState(CharacterStateMachine CharacterStateMachine) : base(CharacterStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    protected override void ElementalSkill_started()
    {
        if (keqingStateMachine.keqingReuseableData.CanThrow())
            return;

        playableCharacterStateMachine.ChangeState(keqingStateMachine.keqingTeleportState);
    }

    protected override void ElementalSkill_canceled()
    {
        if (!keqingStateMachine.keqingReuseableData.CanThrow())
            return;

        Vector3 origin = playableCharacterStateMachine.playableCharacters.GetCenterBound();
        keqingStateMachine.keqingReuseableData.targetPosition = origin + playableCharacterStateMachine.playableCharacters.transform.forward * keqingStateMachine.keqingReuseableData.Range;
        playableCharacterStateMachine.ChangeState(keqingStateMachine.keqingThrowState);
    }

    protected override void ElementalSkill_performed()
    {
        if (!keqingStateMachine.keqingReuseableData.CanThrow())
            return;

        playableCharacterStateMachine.ChangeState(keqingStateMachine.keqingAimState);
    }

    protected KeqingStateMachine keqingStateMachine
    {
        get
        {
            return (KeqingStateMachine)swordCharacterStateMachine;
        }
    }
}
