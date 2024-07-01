using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementalBurstControlState : ElementalControlBaseState
{
    public override void OnEnable()
    {
        base.OnEnable();
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalBurst.performed += ElementalBurst_performed;
    }

    private bool CanTransitToElementalBurstState()
    {
        return CanTransitToAnyElementalState() && playableCharacterStateMachine.playableCharacters.playableCharacterDataStat.CanUseElementalBurst() &&
            !playableCharacterStateMachine.IsSkillCasting();
    }

    protected virtual void ElementalBurst_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!CanTransitToElementalBurstState())
            return;

        ElementalBurst_performed();
    }

    private void ElementalBurst_performed() // go to the animation state as always
    {
        playableCharacterStateMachine.ChangeState(elementalBurst.playerElementalBurstState);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalBurst.performed -= ElementalBurst_performed;
    }

    public ElementalBurstStateMachine elementalBurst
    {
        get
        {
            return skill as ElementalBurstStateMachine;
        }
    }


    public ElementalBurstControlState(SkillStateMachine skills) : base(skills)
    {
    }
}
