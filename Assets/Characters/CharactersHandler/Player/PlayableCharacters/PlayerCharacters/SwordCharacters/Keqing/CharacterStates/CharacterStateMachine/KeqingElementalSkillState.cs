using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeqingElementalSkillState : SwordElementalSkillState
{
    private const float elementalfusionTime = 5f;
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

    public KeqingElementalSkillState(Skill Skill) : base(Skill)
    {
        Range = stellarRestoration.stellarRestorationReusableData.ElementalSkillRange;
    }

    public StellarRestoration stellarRestoration
    {
        get
        {
            return skill as StellarRestoration;
        }
    }
}
