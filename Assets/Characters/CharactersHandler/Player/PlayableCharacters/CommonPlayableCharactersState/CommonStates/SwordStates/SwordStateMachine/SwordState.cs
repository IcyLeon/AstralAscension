using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordState : PlayerCharacterState
{
    public SwordState(PlayableCharacterStateMachine pcs) : base(pcs)
    {
    }

    protected override void SubscribeInputs()
    {
        base.SubscribeInputs();
        playableCharacterStateMachine.player.playerInputAction.ElementalSkill.performed += ElementalSkill_performed;
        playableCharacterStateMachine.player.playerInputAction.ElementalSkill.started += ElementalSkill_started;
        playableCharacterStateMachine.player.playerInputAction.ElementalBurst.performed += ElementalBurst_performed;
    }

    protected override void UnsubscribeInputs()
    {
        base.UnsubscribeInputs();
        playableCharacterStateMachine.player.playerInputAction.ElementalSkill.performed -= ElementalSkill_performed;
        playableCharacterStateMachine.player.playerInputAction.ElementalSkill.started -= ElementalSkill_started;
        playableCharacterStateMachine.player.playerInputAction.ElementalBurst.performed -= ElementalBurst_performed;
    }

    protected virtual void ElementalSkill_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
    }

    protected virtual void ElementalSkill_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
    }
    protected virtual void ElementalBurst_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
    }
}
