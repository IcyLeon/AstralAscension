using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIRig_PlayableCharacters : POIRig
{
    private PlayableCharacters PlayableCharacters;
    private Coroutine headDisableCoroutine;
    private bool HeadMoveDisabled;

    protected override void Awake()
    {
        base.Awake();
        HeadMoveDisabled = false;
        PlayableCharacters = GetComponentInParent<PlayableCharacters>();
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
        if (PlayableCharacters == null)
            return;

        PlayableCharacters.OnTakeDamage += OnDamageHit;
    }

    private void UnsubscribeEvents()
    {
        if (PlayableCharacters == null)
            return;

        PlayableCharacters.OnTakeDamage -= OnDamageHit;
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
        if (PlayableCharacters == null)
            return false;

        return !PlayableCharacters.PlayableCharacterStateMachine.IsSkillCasting() && !HeadMoveDisabled;
    }
}
