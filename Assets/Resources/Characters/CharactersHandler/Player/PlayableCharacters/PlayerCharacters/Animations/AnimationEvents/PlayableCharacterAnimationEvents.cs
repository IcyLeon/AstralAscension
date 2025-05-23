using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayableCharacterAnimationEvents : CharacterAnimationEvents
{
    protected PlayableCharacters playableCharacters
    {
        get
        {
            return Character as PlayableCharacters;
        }
    }

    protected override void OnAnimatorMove()
    {
        if (playableCharacters == null)
            return;

        Player player = playableCharacters.player;
        player.Rb.MovePosition(player.Rb.position + playableCharacters.Animator.deltaPosition);
    }
}
