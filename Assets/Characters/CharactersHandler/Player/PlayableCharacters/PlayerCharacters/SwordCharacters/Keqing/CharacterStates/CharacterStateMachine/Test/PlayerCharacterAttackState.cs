using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterAttackState : IState
{
    protected PlayableCharacterStateMachine playableCharacterStateMachine;

    public PlayerCharacterAttackState(PlayableCharacterStateMachine CharacterStateMachine)
    {
        playableCharacterStateMachine = CharacterStateMachine;
    }

    public virtual void Enter()
    {
        playableCharacterStateMachine.playableCharacterReuseableData.UpdateAttackIdleState();
        PlayerMovementState.OnInterruptState += OnInterruptStateChange;
        StartAnimation(playableCharacterStateMachine.playableCharacters.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.attackingParameter);
        playableCharacterStateMachine.player.PlayerController.playerInputAction.Attack.performed += Attack_performed;
        playableCharacterStateMachine.EntityState.Enter();
    }

    private void Reset()
    {
        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.EntityState);
    }

    private void OnInterruptStateChange(IState IState)
    {
        Reset();
    }

    public virtual void Exit()
    {
        PlayerMovementState.OnInterruptState -= OnInterruptStateChange;
        StopAnimation(playableCharacterStateMachine.playableCharacters.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.attackingParameter);
        playableCharacterStateMachine.player.PlayerController.playerInputAction.Attack.performed -= Attack_performed;
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

    private bool IsAttacking()
    {
        return !playableCharacterStateMachine.playableCharacters.Animator.GetCurrentAnimatorStateInfo(0).IsTag("ATK_IDLE");
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
        if (!IsAttacking())
        {
            if (playableCharacterStateMachine.playableCharacterReuseableData.CanTransitBackToIdleState() 
                || playableCharacterStateMachine.playerStateMachine.IsInState<PlayerMovingState>())
            {
                Reset();
            }
            return;
        }

        playableCharacterStateMachine.playableCharacterReuseableData.UpdateAttackIdleState();
        playableCharacterStateMachine.EntityState.Update();
    }

    public virtual void UpdateTargetRotationData(float angle)
    {
        playableCharacterStateMachine.playerStateMachine.UpdateTargetRotationData(angle);
    }
}
