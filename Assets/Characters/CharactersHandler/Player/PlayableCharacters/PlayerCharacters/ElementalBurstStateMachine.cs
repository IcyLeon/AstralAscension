using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementalBurstStateMachine : SkillStateMachine
{
    public PlayerElementalBurstState playerElementalBurstState { get; protected set; } // all characters will play an animation before transition to other states
    protected ElementalBurstControlState elementalBurstControlBaseState;

    public override void OnEnable()
    {
        if (elementalBurstControlBaseState == null)
            return;

        elementalBurstControlBaseState.OnEnable();
    }

    public override void Update()
    {
        if (elementalBurstControlBaseState == null)
            return;

        if (skillReusableData != null)
            skillReusableData.Update();

        elementalBurstControlBaseState.Update();
    }

    public override void OnDisable()
    {
        if (elementalBurstControlBaseState == null)
            return;

        elementalBurstControlBaseState.OnDisable();
    }

    public override void FixedUpdate()
    {
        if (elementalBurstControlBaseState == null)
            return;

        elementalBurstControlBaseState.FixedUpdate();
    }
    public override void LateUpdate()
    {
        if (elementalBurstControlBaseState == null)
            return;

        elementalBurstControlBaseState.LateUpdate();
    }

    public abstract void InitElementalBurstState();
    public ElementalBurstStateMachine(PlayableCharacterStateMachine playableCharacterStateMachine) : base(playableCharacterStateMachine)
    {
        InitElementalBurstState();
    }
}
