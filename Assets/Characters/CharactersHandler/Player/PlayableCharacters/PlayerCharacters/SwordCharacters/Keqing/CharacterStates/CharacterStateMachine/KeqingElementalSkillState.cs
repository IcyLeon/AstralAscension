using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeqingElementalSkillState : SwordElementalSkillState
{
    protected float Range;

    public KeqingElementalSkillState(PlayableCharacterStateMachine pcs) : base(pcs)
    {
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
