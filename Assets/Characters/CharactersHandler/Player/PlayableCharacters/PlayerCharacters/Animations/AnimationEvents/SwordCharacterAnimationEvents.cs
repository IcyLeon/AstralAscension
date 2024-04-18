using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCharacterAnimationEvents : PlayableCharacterAnimationEvents
{
    [SerializeField] private HitColliders hitColliders;
    private void Hit()
    {
        hitColliders.EnableCollider();
    }
}
