using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class CharacterAnimationEvents : MonoBehaviour
{
    protected Animator animator;
    protected DamageableCharacters character;
    public event Action<Vector3> OnMove;

    private void Awake()
    {
        character = GetComponentInParent<DamageableCharacters>();
        animator = GetComponent<Animator>();
    }
    private void OnCharacterAnimationTransition()
    {
        if (character == null)
            return;

        character.OnAnimationTransition();
    }

    private void OnAnimatorMove()
    {
        if (!animator)
            return;

        OnMove?.Invoke(animator.deltaPosition);
    }
}
