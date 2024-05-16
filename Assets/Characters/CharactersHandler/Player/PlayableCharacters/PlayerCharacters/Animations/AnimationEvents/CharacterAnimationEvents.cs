using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterAnimationEvents : MonoBehaviour
{
    protected DamageableCharacters Character;

    private void Awake()
    {
        Character = GetComponentInParent<DamageableCharacters>();
    }
    private void OnCharacterAnimationTransition()
    {
        if (Character == null || Character.characterStateMachine == null)
            return;

        Character.characterStateMachine.OnAnimationTransition();
    }

    protected virtual void OnAnimatorMove()
    {

    }
}
