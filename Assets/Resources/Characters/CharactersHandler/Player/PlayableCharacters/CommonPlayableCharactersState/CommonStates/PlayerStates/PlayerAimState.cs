using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimState : PlayerGroundedState
{
    private PlayerCameraManager playerCameraManager;
    private AimRigController aimRigController;

    public PlayerAimState(PlayerStateMachine PS) : base(PS)
    {
        playerCameraManager = PS.player.PlayerCameraManager;
        aimRigController = playableCharacters.GetComponentInChildren<AimRigController>();
    }

    public override void Enter()
    {
        base.Enter();
        playerCameraManager.ToggleAimCamera(true);
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
        float angle = playerCameraManager.CameraMain.transform.eulerAngles.y;
        UpdateTargetRotationData(angle);
    }

    public override void Update()
    {
        base.Update();

        if (!playerCameraManager.IsAimCameraActive())
        {
            ExitAimState();
            return;
        }
    }

    private void ExitAimState()
    {
        if (!IsMovementKeyPressed())
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
        playerCameraManager.ToggleAimCamera(false);
        InitBaseRotation();
    }

    private void UpdateTargetWeight(float amt)
    {
        if (aimRigController == null)
            return;

        aimRigController.SmoothRigTransition.SetTargetWeight(amt);
    }
}
