using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCharacterAnimationEvents : PlayableCharacterAnimationEvents
{
    [SerializeField] private PlayableCharacterSwordHitCollider playableCharacterSwordHitCollider;
    private void Hit()
    {
        playableCharacterSwordHitCollider.EnableCollider();
    }
}
