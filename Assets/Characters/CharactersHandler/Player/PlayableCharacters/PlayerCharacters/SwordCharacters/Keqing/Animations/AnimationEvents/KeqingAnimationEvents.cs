using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingAnimationEvents : SwordCharacterAnimationEvents
{
    [SerializeField] private Transform EmitterPivot;

    private void ShootTeleporter()
    {
        if (playableCharacters == null || playableCharacters.PlayableCharacterStateMachine == null)
            return;

        StellarRestoration StellarRestoration = playableCharacters.PlayableCharacterStateMachine.playerElementalSkill as StellarRestoration;

        if (StellarRestoration == null)
            return;

        StellarRestoration.stellarRestorationReusableData.CreateHairpinTeleporter(EmitterPivot);
    }
}
