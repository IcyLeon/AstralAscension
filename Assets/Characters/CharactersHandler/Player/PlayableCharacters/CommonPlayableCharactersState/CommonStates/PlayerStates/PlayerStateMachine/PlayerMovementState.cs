using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementState : IState
{
    protected PlayerStateMachine playerStateMachine;

    public PlayerMovementState(PlayerStateMachine PS)
    {
        playerStateMachine = PS;
        InitBaseRotation();
    }

    protected void InitBaseRotation()
    {
        playerStateMachine.playerData.rotationTime = playerStateMachine.playerData.groundedData.RotationTime;
    }

    protected virtual void SubscribeInputs()
    {
    }


    protected virtual void UnsubscribeInputs()
    {
    }

    public virtual void Enter()
    {
        SubscribeInputs();
    }

    public virtual void Exit()
    {
        UnsubscribeInputs();
    }

    public virtual void FixedUpdate()
    {
        UpdatePhysicsMovement();
    }

    private void UpdatePhysicsMovement()
    {
        if (playerStateMachine.playerData.movementInput == Vector2.zero)
            return;

        Vector2 inputdir = playerStateMachine.playerData.movementInput;
        float angle = Mathf.Atan2(inputdir.x, inputdir.y) * Mathf.Rad2Deg + playerStateMachine.player.CameraManager.CameraMain.transform.eulerAngles.y;
        Vector3 direction = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
        UpdateTargetRotationData(angle);
        RotateTowardsTargetRotation();
        playerStateMachine.player.Rb.AddForce((GetMovementSpeed() * direction) - GetHorizontalVelocity(), ForceMode.VelocityChange);
    }

    protected void ResetVelocity()
    {
        playerStateMachine.player.Rb.velocity = Vector3.zero;
    }
    protected float GetMovementSpeed()
    {
        float movementSpeed = playerStateMachine.playerData.groundedData.BaseSpeed * playerStateMachine.playerData.SpeedModifier;
        return movementSpeed;
    }

    protected void RotateTowardsTargetRotation()
    {
        float currentAngleY = playerStateMachine.player.Rb.transform.eulerAngles.y;
        if (currentAngleY == playerStateMachine.playerData.targetYawRotation)
        {
            return;
        }

        float angle = Mathf.SmoothDampAngle(currentAngleY, playerStateMachine.playerData.targetYawRotation, ref playerStateMachine.playerData.dampedTargetRotationCurrentVelocity, playerStateMachine.playerData.rotationTime - playerStateMachine.playerData.dampedTargetRotationPassedTime);
        playerStateMachine.playerData.dampedTargetRotationPassedTime += Time.deltaTime;

        playerStateMachine.player.Rb.MoveRotation(Quaternion.Euler(0f, angle, 0f));

    }

    protected void UpdateTargetRotationData(float angle)
    {
        playerStateMachine.playerData.targetYawRotation = angle;
        playerStateMachine.playerData.dampedTargetRotationPassedTime = 0f;
    }

    protected Vector3 GetHorizontalVelocity()
    {
        Vector3 vel = playerStateMachine.player.Rb.velocity;
        vel.y = 0;
        return vel;
    }

    protected Vector3 GetVerticalVelocity()
    {
        return new Vector3(playerStateMachine.player.Rb.velocity.x, 0f, playerStateMachine.player.Rb.velocity.z);
    }


    private void ReadMovement()
    {
        playerStateMachine.playerData.movementInput = playerStateMachine.player.playerInputAction.Movement.ReadValue<Vector2>();
    }

    public virtual void OnAnimationTransition()
    {
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
    }

    public virtual void OnCollisionExit(Collision collision)
    {
    }

    public virtual void OnCollisionStay(Collision collision)
    {
    }

    public virtual void Update()
    {
        ReadMovement();
    }
}