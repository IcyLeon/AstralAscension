using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordElementalSkillState : PlayerElementalSkillState
{
    public SwordElementalSkillState(PlayableCharacterStateMachine pcs) : base(pcs)
    {
    }

    protected SwordCharacterStateMachine swordCharacterStateMachine
    {
        get
        {
            return (SwordCharacterStateMachine)playableCharacterStateMachine;
        }
    }


    protected override void OnDamageHit(object source)
    {
        base.OnDamageHit(source);
        swordCharacterStateMachine.ChangeState(swordCharacterStateMachine.swordState);
    }
}