using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingElementalBurstState : PlayerElementalBurstState
{
    public KeqingElementalBurstState(SkillStateMachine Skill) : base(Skill)
    {
    }

    protected SkywardSword skywardSword
    {
        get
        {
            return elementalBurst as SkywardSword;
        }
    }
    protected override void TransitBurstState()
    {
        playableCharacterStateMachine.ChangeState(skywardSword.keqingLaserState);
    }

}
