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
        damageableCharacters.OnDamageHit += OnDamageHit;
    }

    protected virtual void OnDamageHit(object source)
    {
        SetAnimationTrigger("Hit");
    }

    protected virtual void Attack()
    {

    }

    public override void Exit()
    {
        base.Exit();
        damageableCharacters.OnDamageHit -= OnDamageHit;
    }
}
