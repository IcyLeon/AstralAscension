using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageableCharacters : Characters, IDamageable
{
    public delegate void OnDamage(IDamageable source);
    public OnDamage OnDamageHit;

    protected CharacterStateMachine characterStateMachine;

    protected override void Update()
    {
        if (characterStateMachine != null)
        {
            characterStateMachine.Update();
        }
    }

    public void OnCharacterAnimationTransition()
    {
        if (characterStateMachine != null)
        {
            characterStateMachine.OnAnimationTransition();
        }
    }

    protected override void FixedUpdate()
    {
        if (characterStateMachine != null)
        {
            characterStateMachine.FixedUpdate();
        }
    }

    protected override void LateUpdate()
    {
        if (characterStateMachine != null)
        {
            characterStateMachine.LateUpdate();
        }
    }

    public virtual bool IsDead()
    {
        return false;
    }

    public virtual void TakeDamage(IDamageable source, float BaseDamageAmount)
    {
        Animator.SetTrigger("Hit");
        OnDamageHit?.Invoke(source);
        DamageManager.DamageChanged?.Invoke(this, new DamageManager.DamageInfo
        {
            DamageValue = BaseDamageAmount,
            WorldPosition = GetMiddleBound()
        });
    }

    public virtual Vector3 GetMiddleBound()
    {
        return transform.position;
    }
}
