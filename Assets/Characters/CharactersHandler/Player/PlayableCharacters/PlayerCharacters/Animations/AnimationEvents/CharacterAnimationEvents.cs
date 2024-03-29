using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationEvents : MonoBehaviour
{
    [field: SerializeField] public Characters Character { get; private set; }

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
