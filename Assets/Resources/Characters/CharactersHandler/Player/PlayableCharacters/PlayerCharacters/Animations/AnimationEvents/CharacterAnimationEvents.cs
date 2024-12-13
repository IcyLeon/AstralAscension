using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class CharacterAnimationEvents : MonoBehaviour
{
    protected Animator animator;
    protected DamageableCharacters Character;

    private void Awake()
    {
        Character = GetComponentInParent<DamageableCharacters>();
        animator = GetComponent<Animator>();
    }
    private void OnCharacterAnimationTransition()
    {
        if (Character == null)
            return;

        Character.OnAnimationTransition();
    }

    protected virtual void OnAnimatorMove()
    {

    }
}
