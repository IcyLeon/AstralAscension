using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkywardSword : ElementalBurstStateMachine
{
    public KeqingLaserState LaserState()
    {
        return new KeqingLaserState(this);
    }

    protected override SkillReusableData CreateSkillReusableData()
    {
        return new SkywardSwordReusableData(this);
    }

    public override PlayerElementalBurstUnleashedState UnleashedState()
    {
        return new SkywardSwordUnleashedState(this);
    }

    public SkywardSword(PlayableCharacterStateMachine PlayableCharacterStateMachine) : base(PlayableCharacterStateMachine)
    {
    }
}
