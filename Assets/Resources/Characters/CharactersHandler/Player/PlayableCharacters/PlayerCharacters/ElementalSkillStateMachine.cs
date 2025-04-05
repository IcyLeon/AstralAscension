using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementalSkillStateMachine : SkillStateMachine
{
    protected ElementalSkillController elementalSkillController;

    public override void OnEnable()
    {
        if (elementalSkillController == null)
            return;

        elementalSkillController.OnEnable();
    }

    public override void Update()
    {
        if (elementalSkillController == null)
            return;

        if (skillReusableData != null)
            skillReusableData.Update();

        elementalSkillController.Update();
    }

    public override void OnDisable()
    {
        if (elementalSkillController == null)
            return;

        elementalSkillController.OnDisable();
    }

    public override void FixedUpdate()
    {
        if (elementalSkillController == null)
            return;

        elementalSkillController.FixedUpdate();
    }
    public override void LateUpdate()
    {
        if (elementalSkillController == null)
            return;

        elementalSkillController.LateUpdate();
    }

    public abstract void InitElementalSkillState();
    public ElementalSkillStateMachine(PlayableCharacterStateMachine playableCharacterStateMachine) : base(playableCharacterStateMachine)
    {
        InitElementalSkillState();
    }
}
