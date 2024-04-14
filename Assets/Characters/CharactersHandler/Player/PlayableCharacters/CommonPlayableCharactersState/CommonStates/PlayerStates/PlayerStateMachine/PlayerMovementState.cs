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

    public PlayableCharacters playableCharacters
    {
        get
        {
            return playerStateMachine.playableCharacter;
        }
    }
    protected void InitBaseRotation()
    {
        playerStateMachine.playerData.rotationTime = playerStateMachine.playerData.groundedData.BaseRotationTime;
    }

    protected bool IsGrounded()
    {
        Vector3 position = playerStateMachine.player.Rb.position;
        position.y += playerStateMachine.playableCharacter.MainCollider.radius / 2f;

        return Physics.CheckSphere(position, playerStateMachine.playableCharacter.MainCollider.radius, ~LayerMask.GetMask("Player"), QueryTriggerInteraction.Ignore);
    }

    protected bool IsAiming()
    {
        return this is PlayerAimState;
    }

    public virtual void SubscribeInputs()
    {
    }

    protected virtual void Movement_performed(Vector2 movementInput)
    {
    }


    public virtual void UnsubscribeInputs()
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

    public virtual void LateUpdate()
    {

    }

    public virtual void FixedUpdate()
    {
        UpdatePhysicsMovement();
    }

    protected void DecelerateHorizontal()
    {
        Vector3 vel = GetHorizontalVelocity();
        playerStateMachine.player.Rb.AddForce(-vel * playerStateMachine.playerData.DecelerateForce, ForceMode.Acceleration);
    }
     
    protected bool IsMovingHorizontal(float val = 0.1f)
    {
        return GetHorizontalVelocity().magnitude >= val;
    }


    protected bool IsMovingUp(float val = 0.1f)
    {
        return GetVerticalVelocity().y >= val;
    }

    protected void DecelerateVertical()
    {
        Vector3 vel = GetVerticalVelocity();
        playerStateMachine.player.Rb.AddForce(-vel * playerStateMachine.playerData.DecelerateForce, ForceMode.Acceleration);
    }

    protected void StartAnimation(string parameter)
    {
        Characters.StartAnimation(playerStateMachine.playableCharacter.Animator, parameter);
    }

    protected void StopAnimation(string parameter)
    {
        Characters.StopAnimation(playerStateMachine.playableCharacter.Animator, parameter);
    }
    protected void SetAnimationTrigger(string parameter)
    {
        Characters.SetAnimationTrigger(playerStateMachine.playableCharacter.Animator, parameter);
    }

    protected bool IsSkillCasting()
    {
        return playableCharacters.PlayableCharacterStateMachine.IsSkillCasting();
    }

    private void UpdatePhysicsMovement()
    {
        Vector2 inputdir = playerStateMachine.playerData.movementInput;
        if (inputdir == Vector2.zero || playerStateMachine.playerData.SpeedModifier == 0f || IsSkillCasting())
        {
            return;
        }

        if (!IsAiming())
        {
            float angle = Mathf.Atan2(inputdir.x, inputdir.y) * Mathf.Rad2Deg + playerStateMachine.player.CameraManager.CameraMain.transform.eulerAngles.y;
            UpdateTargetRotationData(angle);
            SmoothRotateToTargetRotation();
        }

        playerStateMachine.player.Rb.AddForce((GetMovementSpeed() * GetDirection(playerStateMachine.playerData.targetYawRotation)) - GetHorizontalVelocity(), ForceMode.VelocityChange);
    }

    protected Vector3 GetDirection(float angleInDeg)
    {
        return new Vector3(Mathf.Sin(angleInDeg * Mathf.Deg2Rad), 0f, Mathf.Cos(angleInDeg * Mathf.Deg2Rad));
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

    protected void SmoothRotateToTargetRotation()
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
        return new Vector3(0f, playerStateMachine.player.Rb.velocity.y, 0f);
    }

    private void BlendMovementAnimation()
    {
        PlayableCharacterAnimationSO.CommonPlayableCharacterHash cpc = playerStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters;
        float val = playerStateMachine.playerData.SpeedModifier / playerStateMachine.playerData.groundedData.PlayerSprintData.SpeedModifier;
        
        if (playerStateMachine.playerData.movementInput == Vector2.zero)
        {
            val = 0f;
        }

        playerStateMachine.playableCharacter.Animator.SetFloat(cpc.movementParameters, val, 0.1f, Time.deltaTime);
    }
    private void ReadMovement()
    {
        playerStateMachine.playerData.movementInput = playerStateMachine.player.playerInputAction.Movement.ReadValue<Vector2>();
        
        if (playerStateMachine.playerData.movementInput.magnitude > 0)
            Movement_performed(playerStateMachine.playerData.movementInput);
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
    public virtual void OnTriggerEnter(Collider Collider)
    {
    }

    public virtual void OnTriggerExit(Collider Collider)
    {
    }

    public virtual void OnTriggerStay(Collider Collider)
    {
    }

    public virtual void Update()
    {
        ReadMovement();
        BlendMovementAnimation();
    }
}
