using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillReusableData
{
    public Skill skill { get; }
    public SkillReusableData(Skill skill)
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
