using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
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

    public PlayerData(Player player)
    {
        canSprint = false;
        groundedData = player.PlayerSO.GroundedData;
        airborneData = player.PlayerSO.AirborneData;
        SpeedModifier = 0f;
        DecelerateForce = 0f;
        currentJumpForceMagnitudeXZ = 0f;
        dampedTargetRotationPassedTime = 0;
        consecutiveDashesUsed = 0;
        targetYawRotation = 0;
        rotationTime = 0.14f;
    }
}
