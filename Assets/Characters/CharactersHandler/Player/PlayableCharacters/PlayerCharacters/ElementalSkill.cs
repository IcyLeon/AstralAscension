using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementalSkill : Skill
{
    public ElementalSkillControlState elementalSkillControlBaseState { get; protected set; }

    public abstract void InitElementalSkillState();
    public ElementalSkill(PlayableCharacterStateMachine playableCharacterStateMachine) : base(playableCharacterStateMachine)
    {
        InitElementalSkillState();
    }
}
