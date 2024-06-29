using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementalControlBaseState
{
    protected Skill skill;
    public ElementalControlBaseState(Skill skills)
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

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }


    protected virtual bool CanTransitToAnyElementalState()
    {
        return skill.playableCharacterStateMachine.playerStateMachine.IsInState<PlayerGroundedState>();
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
