using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingAimState : KeqingElementalSkillState
{
    public KeqingAimState(PlayableCharacterStateMachine pcs) : base(pcs)
    {
    }

    protected override void SubscribeInputs()
    {
        base.SubscribeInputs();
        keqingStateMachine.player.playerInputAction.ElementalSkill.canceled += ElementalSkill_canceled;
    }

    private void ElementalSkill_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("tt");
        keqingStateMachine.ChangeState(keqingStateMachine.swordState);
    }

    protected override void UnsubscribeInputs()
    {
        base.UnsubscribeInputs();
        playableCharacterStateMachine.player.playerInputAction.ElementalSkill.canceled -= ElementalSkill_canceled;

    }
}
