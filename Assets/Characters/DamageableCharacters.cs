using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableStats
{
    public float HP;
    public float ATK;
    public float DEF;

    public DamageableStats()
    {
        HP = 0f;
        ATK = 0f;
        DEF = 0f;
    }
}

public abstract class DamageableCharacters : Characters, IDamageable
{
    protected DamageableStats DamageableStats;
    public delegate void OnDamage(object source);
    public OnDamage OnDamageHit;

    protected CharacterStateMachine characterStateMachine;

    public CharacterReuseableData characterReuseableData
    {
        get
        {
            return characterStateMachine.characterReuseableData;
        }
    }

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

    public virtual void TakeDamage(object source, float BaseDamageAmount)
    {
        OnDamageHit?.Invoke(source);
        DamageManager.DamageChanged?.Invoke(this, new DamageManager.DamageInfo
        {
            DamageText = BaseDamageAmount.ToString(),
            WorldPosition = GetMiddleBound()
        });
    }

    public virtual Vector3 GetMiddleBound()
    {
        return transform.position;
    }
}
