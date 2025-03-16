using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingStateMachine : SwordCharacterStateMachine
{
    public KeqingReuseableData keqingReuseableData { get; }

    protected override void InitSkills()
    {
        playerElementalSkillStateMachine = new StellarRestoration(this);
        playerElementalBurstStateMachine = new SkywardSword(this);
    }

    public KeqingStateMachine(Characters characters) : base(characters)
    {
        keqingReuseableData = playableCharacterReuseableData as KeqingReuseableData;
        characterReuseableData = new KeqingReuseableData(2, this);
        playerCharacterAttackState = new KeqingAttackState(this);
    }
}
