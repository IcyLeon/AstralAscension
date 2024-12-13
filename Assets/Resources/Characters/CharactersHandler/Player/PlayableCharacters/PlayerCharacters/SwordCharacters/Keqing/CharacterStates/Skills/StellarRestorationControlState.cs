using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StellarRestorationControlState : ElementalSkillControlState
{
    protected override void ElementalSkill_started()
    {
        if (stellarRestoration.stellarRestorationReusableData.CanThrow())
            return;

        playableCharacterStateMachine.ChangeState(stellarRestoration.keqingTeleportState);
    }

    protected override void ElementalSkill_canceled()
    {
        if (!stellarRestoration.stellarRestorationReusableData.CanThrow())
            return;

        Vector3 origin = playableCharacterStateMachine.playableCharacters.GetCenterBound();
        stellarRestoration.stellarRestorationReusableData.targetPosition = origin + playableCharacterStateMachine.playableCharacters.transform.forward * stellarRestoration.stellarRestorationReusableData.ElementalSkillRange;
        playableCharacterStateMachine.ChangeState(stellarRestoration.keqingThrowState);
    }

    protected override void ElementalSkill_performed()
    {
        if (!stellarRestoration.stellarRestorationReusableData.CanThrow())
            return;

        playableCharacterStateMachine.ChangeState(stellarRestoration.keqingAimState);
    }

    public StellarRestoration stellarRestoration
    {
        get
        {
            return skill as StellarRestoration;
        }
    }

    public StellarRestorationControlState(SkillStateMachine skills) : base(skills)
    {
    }
}
