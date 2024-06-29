using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalSkillControlState : ElementalControlBaseState
{
    public override void Enter()
    {
        base.Enter();
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.canceled += ElementalSkill_canceled;
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.performed += ElementalSkill_performed;
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.started += ElementalSkill_started;
    }

    public override void Exit()
    {
        base.Exit();
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.canceled -= ElementalSkill_canceled;
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.performed -= ElementalSkill_performed;
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.started -= ElementalSkill_started;
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

    public ElementalSkillControlState(Skill skills) : base(skills)
    {
    }
}
