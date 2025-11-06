using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class ElementalSkillStateMachine : SkillStateMachine
{
    public override void OnEnable()
    {
        base.OnEnable();
        playableCharacterStateMachine.player.playerController.playerInputAction.ElementalSkill.canceled += ElementalSkill_canceled;
        playableCharacterStateMachine.player.playerController.playerInputAction.ElementalSkill.performed += ElementalSkill_performed;
        playableCharacterStateMachine.player.playerController.playerInputAction.ElementalSkill.started += ElementalSkill_started;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        playableCharacterStateMachine.player.playerController.playerInputAction.ElementalSkill.canceled -= ElementalSkill_canceled;
        playableCharacterStateMachine.player.playerController.playerInputAction.ElementalSkill.performed -= ElementalSkill_performed;
        playableCharacterStateMachine.player.playerController.playerInputAction.ElementalSkill.started -= ElementalSkill_started;
    }

    private bool CanTransitToElementalSkillState()
    {
        return CanTransitToAnyElementalState() && playableCharacterStateMachine.playableCharacter.playableCharacterDataStat.CanUseElementalSkill() &&
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
    public ElementalSkillStateMachine(PlayableCharacterStateMachine playableCharacterStateMachine) : base(playableCharacterStateMachine)
    {
    }
}
