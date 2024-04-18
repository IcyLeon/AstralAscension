using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterAnimationEvents : MonoBehaviour
{
    [field: SerializeField] public DamageableCharacters Character { get; private set; }

    private void OnCharacterAnimationTransition()
    {
        if (Character == null)
            return;

        Character.OnCharacterAnimationTransition();
    }

    protected virtual void OnAnimatorMove()
    {

    }
}
