using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingAnimationEvents : SwordCharacterAnimationEvents
{
    private void ShootTeleporter()
    {
        if (playableCharacters == null || playableCharacters.playableCharacterStateMachine == null)
            return;

        StellarRestoration StellarRestoration = playableCharacters.playableCharacterStateMachine.playerElementalSkillStateMachine as StellarRestoration;

        if (StellarRestoration == null)
            return;

        StellarRestoration.stellarRestorationReusableData.CreateHairpinTeleporter(GetEmitterPivot());
    }

    private Transform GetEmitterPivot()
    {
        return animator.GetBoneTransform(HumanBodyBones.RightHand);
    }
}
