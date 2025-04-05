using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayableCharacterAttackComboState : PlayableCharacterAttackState
{
    public PlayableCharacterAttackComboState(PlayableCharacterAttackStateMachine PlayableCharacterAttackStateMachine) : base(PlayableCharacterAttackStateMachine)
    {
    }

    public override void OnAnimationTransition()
    {
        base.OnAnimationTransition();
        playableCharacterStateMachine.ChangeState(playableCharacterAttackStateMachine.playableCharacterIdleAttackState);
    }

    public abstract PlayableCharacterAttackComboState GetNextAttackState();
}
