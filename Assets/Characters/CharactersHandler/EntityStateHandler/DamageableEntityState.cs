using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageableEntityState : EntityState
{
    public DamageableEntityState(CharacterStateMachine characterStateMachine) : base(characterStateMachine)
    {
    }

    private DamageableCharacters damageableCharacters
    {
        get
        {
            return characterStateMachine.characters as DamageableCharacters;
        }
    }

    public override void Enter()
    {
        base.Enter();
        damageableCharacters.OnTakeDamage += OnDamageHit;
    }

    protected virtual void OnDamageHit(float BaseDamageAmount)
    {
        if (BaseDamageAmount != 0)
        {
            SetAnimationTrigger("Hit");
        }
    }

    protected virtual void Attack()
    {

    }

    public override void Exit()
    {
        base.Exit();
        damageableCharacters.OnTakeDamage -= OnDamageHit;
    }
}
