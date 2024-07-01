using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeqingState : SwordState
{
    public KeqingState(CharacterStateMachine CharacterStateMachine) : base(CharacterStateMachine)
    {
    }


    protected override void OnSwordHitDamage(PlayableCharacterSwordHitCollider.PlayableCharacterHitEvents e)
    {
        e.damageable.TakeDamage(e.source, e.source.GetElementsSO(), 1f, e.hitPosition);
    }


    protected KeqingStateMachine keqingStateMachine
    {
        get
        {
            return (KeqingStateMachine)swordCharacterStateMachine;
        }
    }
}
