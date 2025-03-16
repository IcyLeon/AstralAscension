using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementalControlBaseState
{
    protected SkillStateMachine skill;
    public ElementalControlBaseState(SkillStateMachine skills)
    {
        skill = skills;
    }

    protected PlayableCharacterStateMachine playableCharacterStateMachine
    {
        get
        {
            return skill.playableCharacterStateMachine;
        }
    }

    public virtual void OnEnable()
    {
    }

    public virtual void OnDisable()
    {
    }


    protected virtual bool CanTransitToAnyElementalState()
    {
        return skill.playableCharacterStateMachine.IsInState<PlayerGroundedState>() || skill.playableCharacterStateMachine.IsAttacking();
    }

    public virtual void FixedUpdate()
    {
    }

    public virtual void LateUpdate()
    {
    }

    public virtual void Update()
    {
    }
}
