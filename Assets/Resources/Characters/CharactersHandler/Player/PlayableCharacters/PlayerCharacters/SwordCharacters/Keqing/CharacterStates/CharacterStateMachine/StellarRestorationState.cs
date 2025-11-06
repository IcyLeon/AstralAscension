using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class StellarRestorationState : PlayerElementalSkillState
{
    private const float elementalfusionTime = 5f;
    protected StellarRestoration stellarRestoration;
    private float elementalfusionElapsedTime;

    protected float Range;

    public override void OnElementalStateEnter()
    {
        base.OnElementalStateEnter();
        ResetFusion();
    }

    public override bool IsElementalStateEnded()
    {
        return elementalfusionElapsedTime <= 0f || base.IsElementalStateEnded();
    }

    public override void UpdateElementalState()
    {
        base.UpdateElementalState();

        elementalfusionElapsedTime -= Time.deltaTime;
    }

    public override void OnElementalStateExit()
    {
        base.OnElementalStateExit();
        ResetFusion();
    }

    private void ResetFusion()
    {
        elementalfusionElapsedTime = elementalfusionTime;
    }

    public StellarRestorationState(SkillStateMachine Skill) : base(Skill)
    {
        stellarRestoration = skill as StellarRestoration;
        Range = stellarRestoration.stellarRestorationReusableData.ElementalSkillRange;
    }
}
