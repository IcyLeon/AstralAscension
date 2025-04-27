using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementalBaseController
{
    protected SkillStateMachine skill;
    public ElementalBaseController(SkillStateMachine skills)
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
        return skill.playableCharacterStateMachine.IsInState<PlayerGroundedState>() || skill.playableCharacterStateMachine.IsInState<PlayableCharacterAttackState>();
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
