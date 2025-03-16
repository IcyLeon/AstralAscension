using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovingState : PlayerGroundedState
{
    public PlayerMovingState(PlayableCharacterStateMachine PS) : base(PS)
    {
    }

    protected virtual void OnStop()
    {

    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(playableCharacterStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.movingParameter);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        SmoothRotateToTargetRotation();
    }

    public override void Update()
    {
        base.Update();

        if (!IsMovementKeyPressed())
        {
            OnStop();
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(playableCharacterStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.movingParameter);
    }
}
