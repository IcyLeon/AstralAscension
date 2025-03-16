using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterAttackState : IState
{
    protected PlayableCharacterStateMachine playableCharacterStateMachine;
    private bool canTransit;

    public PlayerCharacterAttackState(PlayableCharacterStateMachine CharacterStateMachine)
    {
        playableCharacterStateMachine = CharacterStateMachine;
    }

    public virtual void Enter()
    {
        OnEnable();
        canTransit = false;
        StartAnimation(playableCharacterStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.attackParameter);
        playableCharacterStateMachine.playableCharacterReuseableData.UpdateAttackIdleState();
        Attack();
    }

    private void ReadMovement()
    {
        playableCharacterStateMachine.player.playerData.movementInput = playableCharacterStateMachine.playerController.playerInputAction.Movement.ReadValue<Vector2>();
    }

    public virtual void OnEnable()
    {
        playableCharacterStateMachine.playerController.playerInputAction.Attack.performed += Attack_performed;
    }
    public virtual void OnDisable()
    {
        playableCharacterStateMachine.playerController.playerInputAction.Attack.performed -= Attack_performed;
    }

    private void Reset()
    {
        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.playerIdleState);
    }

    public virtual void Exit()
    {
        OnDisable();
        StopAnimation(playableCharacterStateMachine.playableCharacter.PlayableCharacterAnimationSO.CommonPlayableCharacterHashParameters.attackParameter);
    }

    private void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Attack();
    }

    private void Attack()
    {
        playableCharacterStateMachine.playableCharacterReuseableData.DoBasicAttack();
    }

    private bool IsInAttackingAnimation()
    {
        return playableCharacterStateMachine.playableCharacter.Animator.GetCurrentAnimatorStateInfo(0).IsTag("ATK");
    }

    public virtual void FixedUpdate()
    {
        playableCharacterStateMachine.playerIdleState.FixedUpdate();
    }

    public virtual void LateUpdate()
    {
        playableCharacterStateMachine.playerIdleState.LateUpdate();
    }

    public virtual void OnAnimationTransition()
    {
        playableCharacterStateMachine.playerIdleState.OnAnimationTransition();
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        playableCharacterStateMachine.playerIdleState.OnCollisionEnter(collision);
    }

    public virtual void OnCollisionExit(Collision collision)
    {
        playableCharacterStateMachine.playerIdleState.OnCollisionExit(collision);
    }

    public virtual void OnCollisionStay(Collision collision)
    {
        playableCharacterStateMachine.playerIdleState.OnCollisionStay(collision);
    }

    public virtual void OnTriggerEnter(Collider Collider)
    {
        playableCharacterStateMachine.playerIdleState.OnTriggerEnter(Collider);
    }

    public virtual void OnTriggerExit(Collider Collider)
    {
        playableCharacterStateMachine.playerIdleState.OnTriggerExit(Collider);
    }

    public virtual void OnTriggerStay(Collider Collider)
    {
        playableCharacterStateMachine.playerIdleState.OnTriggerStay(Collider);
    }

    public virtual void SetAnimationTrigger(string parameter)
    {
        playableCharacterStateMachine.playerIdleState.SetAnimationTrigger(parameter);
    }

    public virtual void SmoothRotateToTargetRotation()
    {
        playableCharacterStateMachine.player.playerData.SmoothRotateToTargetRotation();
    }

    public virtual void StartAnimation(string parameter)
    {
        playableCharacterStateMachine.playerIdleState.StartAnimation(parameter);
    }

    public virtual void StopAnimation(string parameter)
    {
        playableCharacterStateMachine.playerIdleState.StopAnimation(parameter);
    }

    public virtual void Update()
    {
        ReadMovement();

        if (!IsInAttackingAnimation())
        {
            if (!canTransit)
                return;

            if (playableCharacterStateMachine.playableCharacterReuseableData.CanTransitBackToIdleState() 
                || playableCharacterStateMachine.player.playerData.IsMovementKeyPressed())
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
        //playableCharacterStateMachine.playerIdleState.Update();
    }

    public virtual void UpdateTargetRotationData(float angle)
    {
        playableCharacterStateMachine.player.playerData.UpdateTargetRotationData(angle);
    }
}
