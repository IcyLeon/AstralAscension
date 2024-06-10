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
        PlayableCharacters.OnTakeDamage += OnDamageHit;
    }
    private void OnDamageHit(float BaseDamageAmount)
    {
        //if (headDisableCoroutine != null)
        //    StopCoroutine(headDisableCoroutine);

        //headDisableCoroutine = StartCoroutine(Disable(1f));
    }

    private IEnumerator Disable(float sec)
    {
        HeadMoveDisabled = true;
        yield return new WaitForSeconds(sec);
        HeadMoveDisabled = false;
        headDisableCoroutine = null;
    }

    private void OnDestroy()
    {
        PlayableCharacters.OnTakeDamage -= OnDamageHit;
    }

    private void Start()
    {
        interactSensorReference = PlayableCharacters.player.PlayerInteractSensor;
    }

    protected override bool CanMoveHead()
    {
        return !PlayableCharacters.PlayableCharacterStateMachine.IsSkillCasting() && !HeadMoveDisabled;
    }
}
