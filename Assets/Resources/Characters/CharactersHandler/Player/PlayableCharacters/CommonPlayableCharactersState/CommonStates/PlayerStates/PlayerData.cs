using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    private Player player;
    public GroundedData groundedData { get; private set; }
    public AirborneData airborneData { get; private set; }

    public bool canSprint;
    public float targetYawRotation;
    public float rotationTime;
    public float dampedTargetRotationCurrentVelocity; // do nothing
    public float dampedTargetRotationPassedTime;
    public float DecelerateForce;

    public float currentJumpForceMagnitudeXZ;
    public float SpeedModifier;
    public Vector2 movementInput;

    public int consecutiveDashesUsed;

    public PlayerData(Player Player)
    {
        player = Player;
        canSprint = false;
        groundedData = player.PlayerSO.GroundedData;
        airborneData = player.PlayerSO.AirborneData;
        SpeedModifier = 0f;
        DecelerateForce = 0f;
        movementInput = Vector2.zero;
        currentJumpForceMagnitudeXZ = 0f;
        dampedTargetRotationPassedTime = 0;
        consecutiveDashesUsed = 0;
        targetYawRotation = 0;
        rotationTime = 0.14f;
    }

    public void SmoothRotateToTargetRotation()
    {
        float currentAngleY = player.Rb.transform.eulerAngles.y;
        if (currentAngleY == targetYawRotation)
        {
            return;
        }

        float angle = Mathf.SmoothDampAngle(currentAngleY, targetYawRotation, ref dampedTargetRotationCurrentVelocity, rotationTime - dampedTargetRotationPassedTime);
        dampedTargetRotationPassedTime += Time.deltaTime;
        player.Rb.MoveRotation(Quaternion.Euler(0f, angle, 0f));
    }

    public void UpdateTargetRotationData(float angle)
    {
        float currentAngle = targetYawRotation;

        if (currentAngle == angle)
            return;

       targetYawRotation = angle;
       dampedTargetRotationPassedTime = 0f;
    }

    public bool IsMovementKeyPressed()
    {
        return movementInput != Vector2.zero;
    }
}
