using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkywardSword : ElementalBurst
{
    public KeqingLaserState keqingLaserState { get; }

    public override void InitElementalBurstState()
    {
        skillReusableData = new SkywardSwordReusableData(this);
        elementalBurstControlBaseState = new KeqingElementalBurstControlState(this);
    }
    public SkywardSword(PlayableCharacterStateMachine PlayableCharacterStateMachine) : base(PlayableCharacterStateMachine)
    {
        keqingLaserState = new KeqingLaserState(this);
        playerElementalBurstState = new KeqingElementalBurstState(this);
    }
}
