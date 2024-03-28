using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAirborneState
{
    private bool canRotate = false;
    public PlayerJumpState(PlayerStateMachine PS) : base(PS)
    {
    }

    public override void Enter()
    {
        base.Enter();
        ResetVelocity();

        playerStateMachine.playerData.SpeedModifier = 0f; 

        PlayerJumpData playerJumpData = playerStateMachine.playerData.airborneData.PlayerJumpData;
        playerStateMachine.playerData.DecelerateForce = playerJumpData.JumpDecelerationForce;
        playerStateMachine.playerData.rotationTime = playerJumpData.RotationTime;
        canRotate = playerStateMachine.playerData.movementInput != Vector2.zero;

        Jump();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (canRotate)
        {
            SmoothRotateToTargetRotation();
        }

        if (IsMovingUp())
        {
            DecelerateVertical();
        }
    }

    private void Jump()
    {
        Vector3 forcedir = playerStateMachine.player.transform.forward;
        if (canRotate)
        {
            forcedir = RotateToInputTargetDirection();
        }

        forcedir = forcedir.normalized * playerStateMachine.playerData.currentJumpForceMagnitudeXZ;
        forcedir.y = playerStateMachine.playerData.airborneData.PlayerJumpData.JumpForceY;
        playerStateMachine.player.Rb.AddForce(forcedir, ForceMode.VelocityChange);
    }

    public override void Update()
    {
        base.Update();

        if (IsMovingDown())
        {
            OnFall();
            return;
        }
    }

    private void OnFall()
    {
        playerStateMachine.ChangeState(playerStateMachine.playerFallingState);
    }

    private bool IsMovingDown(float val = 0.1f)
    {
        return GetVerticalVelocity().y < -val;
    }

    public override void Exit()
    {
        base.Exit();
        InitBaseRotation();
        canRotate = false;
    }
}
