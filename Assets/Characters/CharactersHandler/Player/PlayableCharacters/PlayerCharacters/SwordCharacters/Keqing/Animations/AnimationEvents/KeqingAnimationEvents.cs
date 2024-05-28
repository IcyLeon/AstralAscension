using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingAnimationEvents : SwordCharacterAnimationEvents
{
    [SerializeField] private Transform EmitterPivot;
    public delegate void OnHairPinThrow(HairpinTeleporter HairpinTeleporter);
    public static event OnHairPinThrow OnHairPinShoot;

    private void ShootTeleporter()
    {
        if (playableCharacters == null || playableCharacters.PlayableCharacterStateMachine == null)
            return;

        KeqingReuseableData keqingReuseableData = playableCharacters.PlayableCharacterStateMachine.characterReuseableData as KeqingReuseableData;

        if (keqingReuseableData == null)
            return;

        keqingReuseableData.hairpinTeleporter = keqingReuseableData.CreateHairpinTeleporter();

        if (keqingReuseableData.hairpinTeleporter == null)
            return;

        keqingReuseableData.hairpinTeleporter.transform.SetParent(EmitterPivot);
        keqingReuseableData.hairpinTeleporter.transform.localPosition = Vector3.zero;
        keqingReuseableData.hairpinTeleporter.SetTargetLocation(keqingReuseableData.targetPosition);
        OnHairPinShoot?.Invoke(keqingReuseableData.hairpinTeleporter);
    }
}
