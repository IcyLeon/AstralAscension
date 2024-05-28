using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterAttackState : IState
{
    protected PlayableCharacterStateMachine playableCharacterStateMachine;
    private bool canTransit = false;

    public PlayerCharacterAttackState(PlayableCharacterStateMachine CharacterStateMachine)
    {
        playableCharacterStateMachine = CharacterStateMachine;
    }

    public virtual void Enter()
    {
        OnEnable();
        playableCharacterStateMachine.playableCharacterReuseableData.UpdateAttackIdleState();
        PlayerAttackState.OnAttackInterruptState += OnAttackInterruptState;
        playableCharacterStateMachine.EntityState.Enter();
    }

    public virtual void OnEnable()
    {
        playableCharacterStateMachine.player.PlayerController.playerInputAction.Attack.performed += Attack_performed;
    }
    public virtual void OnDisable()
    {
        playableCharacterStateMachine.player.PlayerController.playerInputAction.Attack.performed -= Attack_performed;
    }

    private void Reset()
    {
        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.EntityState);
    }

    private void OnAttackInterruptState()
    {
        Reset();
    }

    public virtual void Exit()
    {
        OnDisable();
        PlayerAttackState.OnAttackInterruptState -= OnAttackInterruptState;
        canTransit = false;
        playableCharacterStateMachine.EntityState.Exit();
    }

    protected virtual void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Attack();
    }

    protected virtual void Attack()
    {
        playableCharacterStateMachine.playableCharacterReuseableData.DoBasicAttack();
    }

    private bool IsInAttackingAnimation()
    {
        return playableCharacterStateMachine.playableCharacters.Animator.GetCurrentAnimatorStateInfo(0).IsTag("ATK");
    }

    public virtual void FixedUpdate()
    {
        playableCharacterStateMachine.EntityState.FixedUpdate();
    }

    public virtual void LateUpdate()
    {
        playableCharacterStateMachine.EntityState.LateUpdate();
    }

    public virtual void OnAnimationTransition()
    {
        playableCharacterStateMachine.EntityState.OnAnimationTransition();
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        playableCharacterStateMachine.EntityState.OnCollisionEnter(collision);
    }

    public virtual void OnCollisionExit(Collision collision)
    {
        playableCharacterStateMachine.EntityState.OnCollisionExit(collision);
    }

    public virtual void OnCollisionStay(Collision collision)
    {
        playableCharacterStateMachine.EntityState.OnCollisionStay(collision);
    }

    public virtual void OnTriggerEnter(Collider Collider)
    {
        playableCharacterStateMachine.EntityState.OnTriggerEnter(Collider);
    }

    public virtual void OnTriggerExit(Collider Collider)
    {
        playableCharacterStateMachine.EntityState.OnTriggerExit(Collider);
    }

    public virtual void OnTriggerStay(Collider Collider)
    {
        playableCharacterStateMachine.EntityState.OnTriggerStay(Collider);
    }

    public virtual void SetAnimationTrigger(string parameter)
    {
        playableCharacterStateMachine.EntityState.SetAnimationTrigger(parameter);
    }

    public virtual void SmoothRotateToTargetRotation()
    {
        playableCharacterStateMachine.playerStateMachine.SmoothRotateToTargetRotation();
    }

    public virtual void StartAnimation(string parameter)
    {
        playableCharacterStateMachine.EntityState.StartAnimation(parameter);
    }

    public virtual void StopAnimation(string parameter)
    {
        playableCharacterStateMachine.EntityState.StopAnimation(parameter);
    }

    public virtual void Update()
    {
        if (!IsInAttackingAnimation())
        {
            if (!canTransit)
                return;

            if (playableCharacterStateMachine.playableCharacterReuseableData.CanTransitBackToIdleState() 
                || playableCharacterStateMachine.playerStateMachine.playerData.movementInput != Vector2.zero)
            {
                Reset();
            }
            return;
        }
        else
        {
            canTransit = true;
        }

        playableCharacterStateMachine.playableCharacterReuseableData.UpdateAttackIdleState();
        playableCharacterStateMachine.EntityState.Update();
    }

    public virtual void UpdateTargetRotationData(float angle)
    {
        playableCharacterStateMachine.playerStateMachine.UpdateTargetRotationData(angle);
    }
}
