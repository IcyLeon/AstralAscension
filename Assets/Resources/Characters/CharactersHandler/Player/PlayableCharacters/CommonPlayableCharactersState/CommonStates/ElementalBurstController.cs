using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementalBurstController : ElementalBaseController
{
    public override void OnEnable()
    {
        base.OnEnable();
        playableCharacterStateMachine.player.playerController.playerInputAction.ElementalBurst.performed += ElementalBurst_performed;
    }

    private bool CanTransitToElementalBurstState()
    {
        return CanTransitToAnyElementalState() && playableCharacterStateMachine.playableCharacter.playableCharacterDataStat.CanUseElementalBurst() &&
            !playableCharacterStateMachine.IsSkillCasting();
    }

    protected virtual void ElementalBurst_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!CanTransitToElementalBurstState())
            return;

        ElementalBurst_performed();
    }

    private void ElementalBurst_performed()
    {
        playableCharacterStateMachine.ChangeState(elementalBurst.playerElementalBurstUnleashedState);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        playableCharacterStateMachine.player.playerController.playerInputAction.ElementalBurst.performed -= ElementalBurst_performed;
    }

    public ElementalBurstStateMachine elementalBurst
    {
        get
        {
            return skill as ElementalBurstStateMachine;
        }
    }


    public ElementalBurstController(SkillStateMachine skills) : base(skills)
    {
    }
}
