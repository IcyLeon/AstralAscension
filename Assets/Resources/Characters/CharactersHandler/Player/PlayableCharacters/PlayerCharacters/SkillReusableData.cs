using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillReusableData
{
    public SkillStateMachine skill { get; }
    public SkillReusableData(SkillStateMachine skill)
    {
        this.skill = skill;
    }

    protected PlayableCharacterStateMachine playableCharacterStateMachine
    {
        get
        {
            return skill.playableCharacterStateMachine;
        }
    }

    public virtual void Update()
    {

    }

    public virtual void ResetData()
    {
    }

    public virtual void OnDestroy()
    {

    }
}
