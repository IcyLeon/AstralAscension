using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordElementalSkillState : PlayerElementalSkillState
{
    public SwordElementalSkillState(Skill Skill) : base(Skill)
    {
    }

    protected SwordCharacterStateMachine swordCharacterStateMachine
    {
        get
        {
            return (SwordCharacterStateMachine)playableCharacterStateMachine;
        }
    }
}