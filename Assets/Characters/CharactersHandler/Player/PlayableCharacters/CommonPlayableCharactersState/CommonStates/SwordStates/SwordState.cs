using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class SwordState : PlayerCharacterState
{
    public SwordState(CharacterStateMachine CharacterStateMachine) : base(CharacterStateMachine)
    {
    }

    public override void OnEnable()
    {
        base.OnEnable();
        swordCharacterStateMachine.SwordHitCollider.EntitySwordHitEvent += EntitySwordHitEvent;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        swordCharacterStateMachine.SwordHitCollider.EntitySwordHitEvent -= EntitySwordHitEvent;
    }

    protected virtual void OnSwordHitDamage(PlayableCharacterSwordHitCollider.PlayableCharacterHitEvents e)
    {
        e.damageable.TakeDamage(e.source, null, 1f, e.hitPosition);
    }

    private void EntitySwordHitEvent(object sender, PlayableCharacterSwordHitCollider.PlayableCharacterHitEvents e)
    {
        OnSwordHitDamage(e);
    }


    protected SwordCharacterStateMachine swordCharacterStateMachine
    {
        get
        {
            return (SwordCharacterStateMachine)playableCharacterStateMachine;
        }
    }
}
