using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayableCharacterAnimationEvents : CharacterAnimationEvents
{
    public PlayableCharacters playableCharacters
    {
        get
        {
            return Character as PlayableCharacters;
        }
    }

    private void OnPlayerAnimationTransition()
    {
        if (playableCharacters == null)
            return;

        playableCharacters.OnPlayerAnimationTransition();
    }

    protected override void OnAnimatorMove()
    {
        Player player = playableCharacters.player;
        player.Rb.MovePosition(player.Rb.position + playableCharacters.Animator.deltaPosition);
    }
}
