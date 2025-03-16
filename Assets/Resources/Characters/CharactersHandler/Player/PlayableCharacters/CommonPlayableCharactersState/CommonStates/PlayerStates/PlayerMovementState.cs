using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerMovementState : IState
{
    protected PlayerStateMachine playerStateMachine;
    protected PlayerController playerController;
    public PlayerMovementState(PlayerStateMachine PS)
    {
        playerStateMachine = PS;
        playerController = playerStateMachine.playerController;
        InitBaseRotation();
    }

    public void SmoothRotateToTargetRotation()
    {
        playerStateMachine.player.playerData.SmoothRotateToTargetRotation();
    }

    public void UpdateTargetRotationData(float angle)
    {
        playerStateMachine.player.playerData.UpdateTargetRotationData(angle);
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
        playerStateMachine.player.playerData.rotationTime = playerStateMachine.player.playerData.groundedData.BaseRotationTime;
    }

    protected bool IsGrounded()
    {
        Vector3 position = playerStateMachine.player.Rb.position;
        position.y += playableCharacters.MainCollider.radius / 2f;

        return Physics.CheckSphere(position, playableCharacters.MainCollider.radius, ~LayerMask.GetMask("Player", "Ignore Raycast"), QueryTriggerInteraction.Ignore);
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
        playerStateMachine.player.Rb.AddForce(-vel * playerStateMachine.player.playerData.DecelerateForce, ForceMode.Acceleration);
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
        playerStateMachine.player.Rb.AddForce(-vel * playerStateMachine.player.playerData.DecelerateForce, ForceMode.Acceleration);
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

    private bool IsNotMoving()
    {
        return !IsMovementKeyPressed()
            || playerStateMachine.player.playerData.SpeedModifier == 0f
            || IsSkillCasting() 
            || playerStateMachine.PlayableCharacterStateMachine.IsAttacking();
    }
    private void UpdatePhysicsMovement()
    {
        if (IsNotMoving())
            return;

        playerStateMachine.player.Rb.AddForce((GetMovementSpeed() * GetDirectionXZ(playerStateMachine.player.playerData.targetYawRotation)) - GetHorizontalVelocity(), ForceMode.VelocityChange);
    }

    protected bool IsMovementKeyPressed()
    {
        return playerStateMachine.player.playerData.IsMovementKeyPressed();
    }

    protected virtual void UpdateRotation()
    {
        if (IsNotMoving())
            return;

        RotateToMovementInputDirection();
    }

    protected void RotateToMovementInputDirection()
    {
        if (!IsMovementKeyPressed())
            return;

        float angle = Vector3Handler.FindAngleByDirection(Vector3.zero, playerStateMachine.player.playerData.movementInput) + playerStateMachine.player.PlayerCameraManager.CameraMain.transform.eulerAngles.y;
        UpdateTargetRotationData(angle);
    }

    protected Vector3 GetDirectionXZ(float angleInDeg)
    {
        return Vector3Handler.FindVector(angleInDeg, 0);
    }

    protected float GetMovementSpeed()
    {
        float movementSpeed = playerStateMachine.player.playerData.groundedData.BaseSpeed * playerStateMachine.player.playerData.SpeedModifier;
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
        float val = playerStateMachine.player.playerData.SpeedModifier / playerStateMachine.player.playerData.groundedData.PlayerSprintData.SpeedModifier;
        
        if (!IsMovementKeyPressed())
        {
            val = 0f;
        }

        playableCharacters.Animator.SetFloat(cpc.movementParameters, val, 0.15f, Time.deltaTime);
    }
    private void ReadMovement()
    {
        playerStateMachine.player.playerData.movementInput = playerController.playerInputAction.Movement.ReadValue<Vector2>();

        if (IsMovementKeyPressed())
        {
            OnMovementKeyPerformed();
        }    
    }

    protected virtual void OnMovementKeyPerformed()
    {

    }

    public virtual void OnEnable()
    {
    }

    public virtual void OnDisable()
    {
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
