using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimState : PlayerGroundedState
{
    private CameraManager cameraManager;
    private float currentAngle;

    public delegate void OnPlayerAim(PlayableCharacterStateMachine PCS);
    public static event OnPlayerAim OnPlayerAimEnter;
    public static event OnPlayerAim OnPlayerAimExit;

    public PlayerAimState(PlayerStateMachine PS) : base(PS)
    {
        cameraManager = PS.player.CameraManager;
    }

    public override void Enter()
    {
        base.Enter();
        cameraManager.ToggleAimCamera(true);
        playerStateMachine.playerData.SpeedModifier = playerStateMachine.playerData.groundedData.PlayerAimData.SpeedModifier;
        playerStateMachine.playerData.rotationTime = 0f;
        OnPlayerAimEnter?.Invoke(playerStateMachine.PlayableCharacterStateMachine);
        playerStateMachine.player.PlayerController.playerInputAction.Jump.Disable();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        SmoothRotateToTargetRotation();
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

        float angle = cameraManager.CameraMain.transform.eulerAngles.y;

        if (angle != currentAngle)
        {
            UpdateTargetRotationData(angle);
            currentAngle = angle;
        }

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


    public override void Exit()
    {
        base.Exit();
        OnPlayerAimExit?.Invoke(playerStateMachine.PlayableCharacterStateMachine);
        cameraManager.ToggleAimCamera(false);
        InitBaseRotation();
        playerStateMachine.player.PlayerController.playerInputAction.Jump.Enable();
    }
}
