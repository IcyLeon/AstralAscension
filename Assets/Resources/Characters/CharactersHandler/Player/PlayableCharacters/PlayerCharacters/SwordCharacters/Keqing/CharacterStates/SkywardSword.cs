using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkywardSword : ElementalBurstStateMachine
{
    public KeqingLaserState keqingLaserState { get; }

    public override void InitElementalBurstState()
    {
        skillReusableData = new SkywardSwordReusableData(this);
        elementalBurstControlBaseState = new SkywardSwordControlState(this);
    }

    public SkywardSword(PlayableCharacterStateMachine PlayableCharacterStateMachine) : base(PlayableCharacterStateMachine)
    {
        keqingLaserState = new KeqingLaserState(this);
        playerElementalBurstUnleashedState = new SkywardSwordUnleashedState(this);
    }
}