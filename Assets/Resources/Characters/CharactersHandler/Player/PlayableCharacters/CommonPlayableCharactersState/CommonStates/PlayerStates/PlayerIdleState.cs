using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(playerStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.idleParameter);
        playerStateMachine.player.playerData.SpeedModifier = 0f;
        playerStateMachine.player.playerData.currentJumpForceMagnitudeXZ = playerStateMachine.player.playerData.airborneData.PlayerJumpData.IdleJumpForceMagnitudeXZ;
        playerStateMachine.ResetVelocity();
    }

    protected override void OnSkillCastUpdate()
    {
    }

    public override void Update()
    {
        base.Update();

        if (!IsMovementKeyPressed())
        {
            return;
        }

        OnMove();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(playerStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.idleParameter);
    }
}
