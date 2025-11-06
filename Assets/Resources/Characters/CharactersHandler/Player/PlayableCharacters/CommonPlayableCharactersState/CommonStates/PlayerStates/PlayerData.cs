using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    private static PlayerData playerDataInstance;
    public static PlayerData instance 
    {
        get
        {
            if (playerDataInstance == null)
            {
                playerDataInstance = new PlayerData();
            }
            return playerDataInstance;
        }
    }
    private Player player;
    public GroundedData groundedData { get; private set; }
    public AirborneData airborneData { get; private set; }
    public float speedModifier;
    public bool canSprint;
    public float targetYawRotation;
    public float rotationTime;
    private float dampedTargetRotationCurrentVelocity; // do nothing
    private float dampedTargetRotationPassedTime;
    public float DecelerateForce;

    public float currentJumpForceMagnitudeXZ;
    public Vector2 movementInput { get; private set; }

    public int consecutiveDashesUsed;

    public PlayerData()
    {
        canSprint = false;
        DecelerateForce = 0f;
        speedModifier = 0f;
        movementInput = Vector2.zero;
        currentJumpForceMagnitudeXZ = 0f;
        dampedTargetRotationPassedTime = 0;
        consecutiveDashesUsed = 0;
        targetYawRotation = 0;
        rotationTime = 0.14f;
    }

    public void SetPlayer(Player Player)
    {
        player = Player;
        groundedData = player.PlayerSO.GroundedData;
        airborneData = player.PlayerSO.AirborneData;
    }

    public void Update()
    {
        ReadMovement();
    }

    private void ReadMovement()
    {
        movementInput = player.playerController.playerInputAction.Movement.ReadValue<Vector2>();
    }

    public void SmoothRotateToTargetRotation()
    {
        float currentAngleY = player.Rb.transform.eulerAngles.y;
        if (currentAngleY == targetYawRotation)
        {
            return;
        }

        float angle = Mathf.SmoothDampAngle(currentAngleY, targetYawRotation, ref dampedTargetRotationCurrentVelocity, rotationTime - dampedTargetRotationPassedTime);
        dampedTargetRotationPassedTime = Mathf.Clamp(dampedTargetRotationPassedTime + Time.fixedDeltaTime, 0f, rotationTime);
        player.Rb.MoveRotation(Quaternion.Euler(0f, angle, 0f));
    }

    public void UpdateTargetRotationData(float angle, bool RotateInstant = false)
    {
        UpdateTargetRotationData(angle);

        if (RotateInstant)
        {
            dampedTargetRotationPassedTime = rotationTime;
        }
    }

    private void UpdateTargetRotationData(float angle)
    {
        if (targetYawRotation == angle)
            return;

        targetYawRotation = angle;
        dampedTargetRotationPassedTime = 0f;
    }

    public bool IsMovementKeyPressed()
    {
        return movementInput != Vector2.zero;
    }
}
