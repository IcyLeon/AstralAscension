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
        UpdatePhysicsMovement();
        SmoothRotateToTargetRotation();
    }

    public override void Update()
    {
        base.Update();
        RotateToMovementInputDirection();

        if (!playableCharacterStateMachine.playerData.IsMovementKeyPressed())
        {
            OnStop();
            return;
        }
    }

    private void UpdatePhysicsMovement()
    {
        float movementSpeed = playableCharacterStateMachine.playerData.groundedData.BaseSpeed * playableCharacterStateMachine.playerData.speedModifier;
        playableCharacterStateMachine.player.Rb.AddForce((movementSpeed * GetDirectionXZ(playableCharacterStateMachine.playerData.targetYawRotation)) - GetHorizontalVelocity(), ForceMode.VelocityChange);
    }

    private void RotateToMovementInputDirection()
    {
        if (!playableCharacterStateMachine.playerData.IsMovementKeyPressed())
            return;

        float angle = Vector3Handler.FindAngleByDirection(Vector3.zero, playableCharacterStateMachine.playerData.movementInput) + playableCharacterStateMachine.player.playerCameraManager.cameraMain.transform.eulerAngles.y;
        UpdateTargetRotationData(angle);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(playableCharacterStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.movingParameter);
    }
}
