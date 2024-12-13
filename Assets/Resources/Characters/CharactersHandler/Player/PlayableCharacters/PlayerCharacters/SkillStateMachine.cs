using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillStateMachine
{
    protected SkillReusableData skillReusableData;
    public PlayableCharacterStateMachine playableCharacterStateMachine { get; }

    public abstract void OnEnable();
    public abstract void OnDisable();
    public abstract void Update();
    public abstract void FixedUpdate();
    public abstract void LateUpdate();
    public virtual void OnDestroy()
    {
        if (skillReusableData != null)
            skillReusableData.OnDestroy();
    }

    public SkillStateMachine(PlayableCharacterStateMachine PlayableCharacterStateMachine)
    {
        playableCharacterStateMachine = PlayableCharacterStateMachine;
    }
}
