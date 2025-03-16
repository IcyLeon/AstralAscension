using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingStateMachine : SwordCharacterStateMachine
{
    public KeqingReuseableData keqingReuseableData
    {
        get
        {
            return (KeqingReuseableData)playableCharacterReuseableData;
        }
    }

    protected override void InitSkills()
    {
        playerElementalSkillStateMachine = new StellarRestoration(this);
        playerElementalBurstStateMachine = new SkywardSword(this);
        EntityState = new KeqingState(this);
    }

    public KeqingStateMachine(Characters characters) : base(characters)
    {
        characterReuseableData = new KeqingReuseableData(2, this);
        playerCharacterAttackState = new KeqingAttackState(this);
        playableCharacterPlungeAttackState = new KeqingPlungeAttackState(this);
    }
}
