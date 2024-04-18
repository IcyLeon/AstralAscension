using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeqingElementalSkillState : SwordElementalSkillState
{
    public KeqingElementalSkillState(PlayableCharacterStateMachine pcs) : base(pcs)
    {
    }

    protected KeqingStateMachine keqingStateMachine
    {
        get
        {
            return (KeqingStateMachine)playableCharacterStateMachine;
        }
    }


    protected Keqing keqing
    {
        get
        {
            return keqingStateMachine.keqing;
        }
    }
}
