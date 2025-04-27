using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStopState : PlayerGroundedState
{
    public PlayerStopState(PlayableCharacterStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(playableCharacterStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.stopParameter);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        SmoothRotateToTargetRotation();

        if (IsMovingHorizontal())
        {
            DecelerateHorizontal();
        }
    }

    public override void Update()
    {
        base.Update();

        if (!playableCharacterStateMachine.playerData.IsMovementKeyPressed())
        {
            return;
        }

        OnMove();
    }

    public override void OnAnimationTransition()
    {
        base.OnAnimationTransition();

        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.playerIdleState);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(playableCharacterStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.stopParameter);
    }
}
