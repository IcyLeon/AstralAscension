using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementalSkillStateMachine : SkillStateMachine
{
    protected ElementalSkillControlState elementalSkillControlBaseState;

    public override void OnEnable()
    {
        if (elementalSkillControlBaseState == null)
            return;

        elementalSkillControlBaseState.OnEnable();
    }

    public override void Update()
    {
        if (elementalSkillControlBaseState == null)
            return;

        if (skillReusableData != null)
            skillReusableData.Update();

        elementalSkillControlBaseState.Update();
    }

    public override void OnDisable()
    {
        if (elementalSkillControlBaseState == null)
            return;

        elementalSkillControlBaseState.OnDisable();
    }

    public override void FixedUpdate()
    {
        if (elementalSkillControlBaseState == null)
            return;

        elementalSkillControlBaseState.FixedUpdate();
    }
    public override void LateUpdate()
    {
        if (elementalSkillControlBaseState == null)
            return;

        elementalSkillControlBaseState.LateUpdate();
    }

    public abstract void InitElementalSkillState();
    public ElementalSkillStateMachine(PlayableCharacterStateMachine playableCharacterStateMachine) : base(playableCharacterStateMachine)
    {
        InitElementalSkillState();
    }
}
