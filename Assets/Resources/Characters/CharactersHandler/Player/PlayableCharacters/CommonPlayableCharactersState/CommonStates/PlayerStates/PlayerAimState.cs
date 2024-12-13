using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimState : PlayerGroundedState
{
    private CameraManager cameraManager;
    private float currentAngle;
    private AimRigController aimRigController;

    public PlayerAimState(PlayerStateMachine PS) : base(PS)
    {
        currentAngle = 0f;
        cameraManager = PS.player.CameraManager;
        aimRigController = playableCharacters.GetComponentInChildren<AimRigController>();
    }

    public override void Enter()
    {
        base.Enter();
        cameraManager.ToggleAimCamera(true);
        playerStateMachine.playerData.SpeedModifier = playerStateMachine.playerData.groundedData.PlayerAimData.SpeedModifier;
        playerStateMachine.playerData.rotationTime = 0f;

        UpdateTargetWeight(1f);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        SmoothRotateToTargetRotation();
    }

    protected override void OnAttackUpdate()
    {

    }

    protected override void OnSkillCastUpdate()
    {
    }

    protected override void UpdateRotation()
    {
        float angle = cameraManager.CameraMain.transform.eulerAngles.y;

        if (angle != currentAngle)
        {
            UpdateTargetRotationData(angle);
            currentAngle = angle;
        }
    }

    public override void Update()
    {
        base.Update();

        if (!cameraManager.IsAimCameraActive())
        {
            ExitAimState();
            return;
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

    protected override void Jump_started()
    {
    }

    public override void Exit()
    {
        base.Exit();
        UpdateTargetWeight(0f);
        cameraManager.ToggleAimCamera(false);
        InitBaseRotation();
    }

    private void UpdateTargetWeight(float amt)
    {
        if (aimRigController == null)
            return;

        aimRigController.SmoothRigTransition.SetTargetWeight(amt);
    }
}
