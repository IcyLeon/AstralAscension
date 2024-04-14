using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeqingState : SwordState
{
    public KeqingState(PlayableCharacterStateMachine pcs) : base(pcs)
    {
    }

    public override void SubscribeInputs()
    {
        base.SubscribeInputs();
        keqingStateMachine.player.playerInputAction.ElementalSkill.canceled += ElementalSkill_canceled;
    }

    public override void UnsubscribeInputs()
    {
        base.UnsubscribeInputs();
        keqingStateMachine.player.playerInputAction.ElementalSkill.canceled -= ElementalSkill_canceled;
    }

    private void ElementalSkill_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        keqingStateMachine.ChangeState(keqingStateMachine.keqingThrowState);
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
