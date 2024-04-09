using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeqingState : SwordState
{
    public KeqingState(PlayableCharacterStateMachine pcs) : base(pcs)
    {
    }

    protected override void SubscribeInputs()
    {
        base.SubscribeInputs();
        playableCharacterStateMachine.player.playerInputAction.ElementalSkill.canceled += ElementalSkill_canceled;
    }

    private void ElementalSkill_canceled(InputAction.CallbackContext obj)
    {
        keqingStateMachine.ChangeState(keqingStateMachine.keqingThrowState);
    }

    protected override void UnsubscribeInputs()
    {
        base.UnsubscribeInputs();
        playableCharacterStateMachine.player.playerInputAction.ElementalSkill.canceled -= ElementalSkill_canceled;
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
