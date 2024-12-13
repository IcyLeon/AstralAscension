using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElementalBurstUnleashedState : PlayerElementalBurstState
{
    public PlayerElementalBurstUnleashedState(SkillStateMachine Skill) : base(Skill)
    {
    }

    public override void OnAnimationTransition()
    {
        base.OnAnimationTransition();

        TransitBurstState();
    }

    public ElementalBurstStateMachine elementalBurst
    {
        get
        {
            return skill as ElementalBurstStateMachine;
        }
    }

    protected virtual void TransitBurstState()
    {
        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.EntityState);
    }
}