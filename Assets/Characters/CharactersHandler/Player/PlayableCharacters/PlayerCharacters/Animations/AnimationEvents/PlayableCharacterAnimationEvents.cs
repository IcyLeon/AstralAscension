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


    private void OnPlayerAnimationTransition()
    {
        if (playableCharacters == null || playableCharacters.PlayableCharacterStateMachine == null)
            return;

        playableCharacters.PlayableCharacterStateMachine.playerStateMachine.OnAnimationTransition();
    }

    protected override void OnAnimatorMove()
    {
        if (playableCharacters == null)
            return;

        Player player = playableCharacters.player;
        player.Rb.MovePosition(player.Rb.position + playableCharacters.Animator.deltaPosition);
    }
}
