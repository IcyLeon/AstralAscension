using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkywardSwordUnleashedState : PlayerElementalBurstUnleashedState
{
    private SkywardSword skywardSword;

    public SkywardSwordUnleashedState(SkywardSword SkywardSword) : base(SkywardSword)
    {
        skywardSword = SkywardSword;
    }

    protected override void TransitBurstState()
    {
        playableCharacterStateMachine.ChangeState(skywardSword.LaserState());
    }

}
