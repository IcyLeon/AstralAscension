using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageableCharacters : Characters, IDamageable
{
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

}
