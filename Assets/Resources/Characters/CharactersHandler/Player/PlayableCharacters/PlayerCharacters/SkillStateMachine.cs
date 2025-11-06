using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class SkillStateMachine
{
    private SkillReusableData skillReusableData;
    public PlayableCharacterStateMachine playableCharacterStateMachine { get; }


    public virtual void OnEnable()
    {
    }

    public virtual void Update()
    {
        UpdateData();
    }

    private void UpdateData()
    {
        if (skillReusableData == null)
            return;

        skillReusableData.Update();
    }

    public virtual void OnDisable()
    {
    }

    public virtual void FixedUpdate()
    {
    }
    protected virtual bool CanTransitToAnyElementalState()
    {
        return playableCharacterStateMachine.IsInState<PlayerGroundedState>() || playableCharacterStateMachine.IsInState<PlayableCharacterAttackState>();
    }

    public virtual void LateUpdate()
    {
    }

    protected abstract SkillReusableData CreateSkillReusableData();

    public virtual void OnDestroy()
    {
        if (skillReusableData != null)
            skillReusableData.OnDestroy();
    }


    public SkillStateMachine(PlayableCharacterStateMachine PlayableCharacterStateMachine)
    {
        playableCharacterStateMachine = PlayableCharacterStateMachine;
        skillReusableData = CreateSkillReusableData();
    }
}
