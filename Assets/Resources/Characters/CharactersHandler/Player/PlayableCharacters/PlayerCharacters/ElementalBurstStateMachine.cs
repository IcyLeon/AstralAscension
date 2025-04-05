using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementalBurstStateMachine : SkillStateMachine
{
    public PlayerElementalBurstUnleashedState playerElementalBurstUnleashedState { get; protected set; }
    protected ElementalBurstController elementalBurstController;

    public override void OnEnable()
    {
        if (elementalBurstController == null)
            return;

        elementalBurstController.OnEnable();
    }

    public override void Update()
    {
        if (elementalBurstController == null)
            return;

        if (skillReusableData != null)
            skillReusableData.Update();

        elementalBurstController.Update();
    }

    public override void OnDisable()
    {
        if (elementalBurstController == null)
            return;

        elementalBurstController.OnDisable();
    }

    public override void FixedUpdate()
    {
        if (elementalBurstController == null)
            return;

        elementalBurstController.FixedUpdate();
    }
    public override void LateUpdate()
    {
        if (elementalBurstController == null)
            return;

        elementalBurstController.LateUpdate();
    }

    public abstract void InitElementalBurstState();

    public ElementalBurstStateMachine(PlayableCharacterStateMachine playableCharacterStateMachine) : base(playableCharacterStateMachine)
    {
        InitElementalBurstState();
    }
}
