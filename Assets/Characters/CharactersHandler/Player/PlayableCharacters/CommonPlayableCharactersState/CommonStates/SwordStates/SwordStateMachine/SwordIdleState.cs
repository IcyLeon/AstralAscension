using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwordIdleState : PlayerCharacterState
{
    public SwordIdleState(PlayableCharacterStateMachine pcs) : base(pcs)
    {
    }

    protected override void ElementalSkill_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.playerElementalStateHandler);
    }

    protected override void ElementalSkill_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.playerElementalStateHandler);
    }

    protected override void ElementalBurst_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.playerElementalStateHandler);
    }
}
