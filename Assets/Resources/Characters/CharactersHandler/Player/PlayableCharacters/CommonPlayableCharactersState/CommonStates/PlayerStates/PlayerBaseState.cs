using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerBaseState : IState
{
    protected PlayableCharacterStateMachine playableCharacterStateMachine;
    public PlayerBaseState(PlayableCharacterStateMachine PS)
    {
        playableCharacterStateMachine = PS;
        InitBaseRotation();
    }

    public void SmoothRotateToTargetRotation()
    {
        playableCharacterStateMachine.playerData.SmoothRotateToTargetRotation();
    }

    public void UpdateTargetRotationData(float angle)
    {
        playableCharacterStateMachine.playerData.UpdateTargetRotationData(angle);
    }

    public PlayableCharacters playableCharacter
    {
        get
        {
            return playableCharacterStateMachine.playableCharacter;
        }
    }


    protected void InitBaseRotation()
    {
        playableCharacterStateMachine.playerData.rotationTime = playableCharacterStateMachine.playerData.groundedData.BaseRotationTime;
    }

    protected bool IsGrounded()
    {
        Vector3 position = playableCharacterStateMachine.player.Rb.position;
        position.y += playableCharacter.MainCollider.radius / 2f;

        return Physics.CheckSphere(position, playableCharacter.MainCollider.radius, ~LayerMask.GetMask("Player", "Ignore Raycast"), QueryTriggerInteraction.Ignore);
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
    }

    protected void DecelerateHorizontal()
    {
        Vector3 vel = GetHorizontalVelocity();
        playableCharacterStateMachine.player.Rb.AddForce(-vel * playableCharacterStateMachine.playerData.DecelerateForce, ForceMode.Acceleration);
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
        playableCharacterStateMachine.player.Rb.AddForce(-vel * playableCharacterStateMachine.playerData.DecelerateForce, ForceMode.Acceleration);
    }

    public void StartAnimation(string parameter)
    {
        playableCharacterStateMachine.StartAnimation(parameter);
    }

    public void StopAnimation(string parameter)
    {
        playableCharacterStateMachine.StopAnimation(parameter);
    }
    public void SetAnimationTrigger(string parameter)
    {
        playableCharacterStateMachine.SetAnimationTrigger(parameter);
    }

    protected bool IsSkillCasting()
    {
        return playableCharacterStateMachine.IsSkillCasting();
    }

    protected Vector3 GetDirectionXZ(float angleInDeg)
    {
        return Vector3Handler.FindVector(angleInDeg, 0);
    }

    protected Vector3 GetHorizontalVelocity()
    {
        Vector3 vel = playableCharacterStateMachine.player.Rb.velocity;
        vel.y = 0;
        return vel;
    }

    protected Vector3 GetVerticalVelocity()
    {
        return new Vector3(0f, playableCharacterStateMachine.player.Rb.velocity.y, 0f);
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
        if (!playableCharacter.IsDead())
            return;

        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.playerDeadState);
    }

    private void BlendMovementAnimation()
    {
        PlayableCharacterAnimationSO.CommonPlayableCharacterHash cpc = playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters;
        playableCharacter.Animator.SetFloat(cpc.movementParameters, playableCharacterStateMachine.playerData.speedModifier, 0.15f, Time.deltaTime);
    }


    public virtual void Update()
    {
        OnDeadUpdate();
        BlendMovementAnimation();
    }
}
