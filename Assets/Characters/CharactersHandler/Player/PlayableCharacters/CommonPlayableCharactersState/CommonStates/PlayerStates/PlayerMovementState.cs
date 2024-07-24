using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerMovementState : IState
{
    protected PlayerStateMachine playerStateMachine;

    public PlayerMovementState(PlayerStateMachine PS)
    {
        playerStateMachine = PS;
        InitBaseRotation();
    }

    public void SmoothRotateToTargetRotation()
    {
        playerStateMachine.SmoothRotateToTargetRotation();
    }

    public void UpdateTargetRotationData(float angle)
    {
        playerStateMachine.UpdateTargetRotationData(angle);
    }

    public PlayableCharacters playableCharacters
    {
        get
        {
            return playerStateMachine.playableCharacter;
        }
    }

    public virtual void OnEnable()
    {
    }

    public virtual void OnDisable()
    {
    }

    protected void InitBaseRotation()
    {
        playerStateMachine.playerData.rotationTime = playerStateMachine.playerData.groundedData.BaseRotationTime;
    }

    protected bool IsGrounded()
    {
        Vector3 position = playerStateMachine.player.Rb.position;
        position.y += playableCharacters.MainCollider.radius / 2f;

        return Physics.CheckSphere(position, playableCharacters.MainCollider.radius, ~LayerMask.GetMask("Player", "Ignore Raycast"), QueryTriggerInteraction.Ignore);
    }

    protected virtual void Movement_performed(Vector2 movementInput)
    {
    }

    public virtual void Enter()
    {
        OnEnable();
    }

    public virtual void Exit()
    {
        OnDisable();
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

    public void StartAnimation(string parameter)
    {
        playerStateMachine.PlayableCharacterStateMachine.StartAnimation(parameter);
    }

    public void StopAnimation(string parameter)
    {
        playerStateMachine.PlayableCharacterStateMachine.StopAnimation(parameter);
    }
    public void SetAnimationTrigger(string parameter)
    {
        playerStateMachine.PlayableCharacterStateMachine.SetAnimationTrigger(parameter);
    }

    protected bool IsSkillCasting()
    {
        return playerStateMachine.PlayableCharacterStateMachine.IsSkillCasting();
    }

    private bool CanUpdateMovement()
    {
        return playerStateMachine.playerData.movementInput == Vector2.zero
            || playerStateMachine.playerData.SpeedModifier == 0f
            || IsSkillCasting() || playerStateMachine.PlayableCharacterStateMachine.IsAttacking();
    }
    private void UpdatePhysicsMovement()
    {
        if (CanUpdateMovement())
            return;

        playerStateMachine.player.Rb.AddForce((GetMovementSpeed() * GetDirectionXZ(playerStateMachine.playerData.targetYawRotation)) - GetHorizontalVelocity(), ForceMode.VelocityChange);
    }

    protected virtual void UpdateRotation()
    {
        if (CanUpdateMovement())
            return;

        float angle = Vector3Handler.FindAngleByDirection(Vector3.zero, playerStateMachine.playerData.movementInput) + playerStateMachine.player.CameraManager.CameraMain.transform.eulerAngles.y;
        UpdateTargetRotationData(angle);
    }

    protected Vector3 GetDirectionXZ(float angleInDeg)
    {
        return Vector3Handler.FindVector(angleInDeg, 0);
    }

    protected float GetMovementSpeed()
    {
        float movementSpeed = playerStateMachine.playerData.groundedData.BaseSpeed * playerStateMachine.playerData.SpeedModifier;
        return movementSpeed;
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
        PlayableCharacterAnimationSO.CommonPlayableCharacterHash cpc = playableCharacters.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters;
        float val = playerStateMachine.playerData.SpeedModifier / playerStateMachine.playerData.groundedData.PlayerSprintData.SpeedModifier;
        
        if (playerStateMachine.playerData.movementInput == Vector2.zero)
        {
            val = 0f;
        }

        playableCharacters.Animator.SetFloat(cpc.movementParameters, val, 0.15f, Time.deltaTime);
    }
    private void ReadMovement()
    {
        playerStateMachine.playerData.movementInput = playerStateMachine.player.PlayerController.playerInputAction.Movement.ReadValue<Vector2>();
        
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

    protected virtual void OnDeadUpdate()
    {
        if (!playableCharacters.IsDead())
            return;

        playerStateMachine.ChangeState(playerStateMachine.playerDeadState);
    }

    public virtual void Update()
    {
        OnDeadUpdate();
        UpdateRotation();
        ReadMovement();
        BlendMovementAnimation();
    }
}
