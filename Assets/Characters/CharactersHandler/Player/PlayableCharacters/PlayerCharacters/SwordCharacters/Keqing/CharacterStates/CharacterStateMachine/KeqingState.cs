using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeqingState : SwordState
{
    public KeqingState(PlayableCharacterStateMachine pcs) : base(pcs)
    {
    }

    protected override void ElementalBurst_performed(InputAction.CallbackContext obj)
    {
    }

    protected override void ElementalSkill_performed(InputAction.CallbackContext obj)
    {
        keqingStateMachine.ChangeState(keqingStateMachine.keqingAimState);
    }

    protected KeqingStateMachine keqingStateMachine
    {
        get
        {
            return (KeqingStateMachine)playableCharacterStateMachine;
        }
    }
}
