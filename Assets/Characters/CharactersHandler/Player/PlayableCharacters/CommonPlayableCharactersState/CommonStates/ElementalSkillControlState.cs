using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementalSkillControlState : ElementalControlBaseState
{
    public override void OnEnable()
    {
        base.OnEnable();
        playableCharacterStateMachine.playerController.playerInputAction.ElementalSkill.canceled += ElementalSkill_canceled;
        playableCharacterStateMachine.playerController.playerInputAction.ElementalSkill.performed += ElementalSkill_performed;
        playableCharacterStateMachine.playerController.playerInputAction.ElementalSkill.started += ElementalSkill_started;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        playableCharacterStateMachine.playerController.playerInputAction.ElementalSkill.canceled -= ElementalSkill_canceled;
        playableCharacterStateMachine.playerController.playerInputAction.ElementalSkill.performed -= ElementalSkill_performed;
        playableCharacterStateMachine.playerController.playerInputAction.ElementalSkill.started -= ElementalSkill_started;
    }

    private bool CanTransitToElementalSkillState()
    {
        return CanTransitToAnyElementalState() && playableCharacterStateMachine.playableCharacters.playableCharacterDataStat.CanUseElementalSkill() &&
            !playableCharacterStateMachine.IsSkillCasting();
    }

    private void ElementalSkill_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!CanTransitToElementalSkillState())
            return;

        ElementalSkill_started();
    }

    private void ElementalSkill_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!CanTransitToElementalSkillState())
            return;

        ElementalSkill_canceled();
    }

    private void ElementalSkill_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!CanTransitToElementalSkillState())
            return;

        ElementalSkill_performed();
    }

    protected virtual void ElementalSkill_started()
    {
    }
    protected virtual void ElementalSkill_canceled()
    {
    }

    protected virtual void ElementalSkill_performed()
    {

    }

    public ElementalSkillControlState(SkillStateMachine skills) : base(skills)
    {
    }
}
