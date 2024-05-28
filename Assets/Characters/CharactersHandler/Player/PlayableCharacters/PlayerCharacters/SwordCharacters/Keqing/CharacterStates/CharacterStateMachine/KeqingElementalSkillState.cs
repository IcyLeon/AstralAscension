using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeqingElementalSkillState : SwordElementalSkillState
{
    protected AimRigController aimRigController;
    protected float Range;

    public KeqingElementalSkillState(PlayableCharacterStateMachine pcs) : base(pcs)
    {
        aimRigController = keqingStateMachine.playableCharacters.GetComponentInChildren<AimRigController>();
        Range = keqingStateMachine.keqingReuseableData.Range;
    }

    protected KeqingStateMachine keqingStateMachine
    {
        get
        {
            return (KeqingStateMachine)playableCharacterStateMachine;
        }
    }
}
