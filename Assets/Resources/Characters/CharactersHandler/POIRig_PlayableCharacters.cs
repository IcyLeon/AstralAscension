using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIRig_PlayableCharacters : POIRig
{
    private PlayableCharacters playableCharacter;
    private Coroutine headDisableCoroutine;
    private bool HeadMoveDisabled;

    protected override void Awake()
    {
        base.Awake();
        HeadMoveDisabled = false;
        playableCharacter = GetComponentInParent<PlayableCharacters>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        SubscribeEvents();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        if (playableCharacter == null)
            return;

        playableCharacter.OnTakeDamage += OnDamageHit;
    }

    private void UnsubscribeEvents()
    {
        if (playableCharacter == null)
            return;

        playableCharacter.OnTakeDamage -= OnDamageHit;
    }

    private void OnDamageHit(float BaseDamageAmount)
    {
        if (headDisableCoroutine != null)
            StopCoroutine(headDisableCoroutine);

        headDisableCoroutine = StartCoroutine(Disable(1f));
    }

    private IEnumerator Disable(float sec)
    {
        HeadMoveDisabled = true;
        yield return new WaitForSeconds(sec);
        HeadMoveDisabled = false;
        headDisableCoroutine = null;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        UnsubscribeEvents();
    }

    protected override bool CanMoveHead()
    {
        if (playableCharacter == null || playableCharacter.playableCharacterStateMachine == null)
            return false;

        return !playableCharacter.playableCharacterStateMachine.IsSkillCasting() && !HeadMoveDisabled;
    }
}
