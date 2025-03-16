using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDeadState : PlayerMovementState
{
    public PlayerDeadState(PlayableCharacterStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        playableCharacterStateMachine.player.playerData.SpeedModifier = 0f;

    }

    protected override void OnDeadUpdate()
    {
        if (playableCharacter.IsDead())
            return;

        if (!IsMovementKeyPressed())
        {
            playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.playerIdleState);
            return;
        }
        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.playerRunState);
    }
}
