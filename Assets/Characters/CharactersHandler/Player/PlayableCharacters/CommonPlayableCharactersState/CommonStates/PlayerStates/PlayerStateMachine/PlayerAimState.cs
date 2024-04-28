using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimState : PlayerGroundedState
{
    private CameraManager cameraManager;
    private float currentAngle;
    public PlayerAimState(PlayerStateMachine PS) : base(PS)
    {
        cameraManager = playerStateMachine.player.CameraManager;
    }

    public override void Enter()
    {
        base.Enter();
        playerStateMachine.playerData.SpeedModifier = playerStateMachine.playerData.groundedData.PlayerAimData.SpeedModifier;
        playerStateMachine.playerData.rotationTime = 0f;
        cameraManager.ToggleAimCamera(true);
        playerStateMachine.player.PlayerController.playerInputAction.Jump.Disable();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        playerStateMachine.SmoothRotateToTargetRotation();
    }

    protected override void OnAttackUpdate()
    {

    }

    protected override void OnSkillCast()
    {

    }

    public override void Update()
    {
        base.Update();

        float angle = Mathf.Atan2(cameraManager.CameraMain.transform.forward.x,
            cameraManager.CameraMain.transform.forward.z) * Mathf.Rad2Deg;

        if (angle != currentAngle)
        {
            UpdateTargetRotationData(angle);
            currentAngle = angle;
        }

        if (!cameraManager.IsAimCameraActive())
        {
            ExitAimState();
        }
    }

    private void ExitAimState()
    {
        if (playerStateMachine.playerData.movementInput == Vector2.zero)
        {
            playerStateMachine.ChangeState(playerStateMachine.playerIdleState);
            return;
        }
        OnMove();
        return;
    }


    public override void Exit()
    {
        base.Exit();
        cameraManager.ToggleAimCamera(false);
        InitBaseRotation();
        playerStateMachine.player.PlayerController.playerInputAction.Jump.Enable();
    }
}
