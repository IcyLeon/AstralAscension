using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimController
{
    private PlayableCharacters playableCharacter;
    private PlayerCameraManager playerCameraManager;
    private AimRigController aimRigController;
    private PlayerData playerData;

    public PlayerAimController(PlayableCharacters PlayableCharacter)
    {
        playerData = PlayerData.instance;
        playableCharacter = PlayableCharacter;
        playerCameraManager = playableCharacter.player.PlayerCameraManager;
        aimRigController = playableCharacter.GetComponentInChildren<AimRigController>();
    }

    public void Enter()
    {
        ToggleAimCamera(true);
    }

    public void Exit(float time = 0f)
    {
        ToggleAimCamera(false, time);
    }

    private void ToggleAimCamera(bool enable, float time = 0f)
    {
        float targetWeight = 1f;

        if (!enable)
        {
            targetWeight = 0f;
        }

        UpdateTargetWeight(targetWeight);
        playerCameraManager.ToggleAimCamera(enable, time);
    }

    public void FixedUpdate()
    {
        playerData.SmoothRotateToTargetRotation();
    }

    public void Update()
    {
        float angle = playerCameraManager.CameraMain.transform.eulerAngles.y;
        playerData.UpdateTargetRotationData(angle);
    }

    private void UpdateTargetWeight(float amt)
    {
        if (aimRigController == null)
            return;

        aimRigController.SmoothRigTransition.SetTargetWeight(amt);
    }
}
