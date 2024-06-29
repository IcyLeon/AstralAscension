using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill
{
    public SkillReusableData skillReusableData { get; protected set; }
    public PlayableCharacterStateMachine playableCharacterStateMachine { get; }

    public Skill(PlayableCharacterStateMachine PlayableCharacterStateMachine)
    {
        playableCharacterStateMachine = PlayableCharacterStateMachine;
    }
}
