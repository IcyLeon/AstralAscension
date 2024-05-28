using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerCharacterState : DamageableEntityState
{
    public PlayerCharacterState(CharacterStateMachine CharacterStateMachine) : base(CharacterStateMachine)
    {
    }

    protected PlayableCharacterStateMachine playableCharacterStateMachine
    {
        get
        {
            return characterStateMachine as PlayableCharacterStateMachine;
        }
    }

    protected override void OnDamageHit(float BaseDamageAmount)
    {
        if (playableCharacterStateMachine.playerStateMachine.IsInState<PlayerAirborneState>())
            return;

        base.OnDamageHit(BaseDamageAmount);
    }

    protected override void Attack()
    {
        if (playableCharacterStateMachine.IsAttacking())
            return;

        if (!playableCharacterStateMachine.playerStateMachine.IsInState<PlayerGroundedState>() ||
            playableCharacterStateMachine.playerStateMachine.IsInState<PlayerDashState>())
            return;

        playableCharacterStateMachine.ChangeState(playableCharacterStateMachine.playerCharacterAttackState);
    }

    public override void UpdateTargetRotationData(float angle)
    {
        playableCharacterStateMachine.playerStateMachine.UpdateTargetRotationData(angle);
    }
    public override void SmoothRotateToTargetRotation()
    {
        playableCharacterStateMachine.playerStateMachine.SmoothRotateToTargetRotation();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void OnEnable()
    {
        base.OnEnable();

        playableCharacterStateMachine.player.PlayerController.playerInputAction.Attack.started += Attack_performed;
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.performed += ElementalSkill_performed;
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.started += ElementalSkill_started;
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalBurst.performed += ElementalBurst_performed;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        playableCharacterStateMachine.player.PlayerController.playerInputAction.Attack.started -= Attack_performed;
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.performed -= ElementalSkill_performed;
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.started -= ElementalSkill_started;
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalBurst.performed -= ElementalBurst_performed;
    }

    protected virtual bool CanTransitToElementalState()
    {
        return playableCharacterStateMachine.playerStateMachine.IsInState<PlayerGroundedState>();
    }


    protected virtual void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Attack();
    }

    protected virtual void ElementalSkill_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
    }

    protected virtual void ElementalSkill_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {

    }

    protected virtual void ElementalBurst_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void OnAnimationTransition()
    {
    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }

    public override void OnCollisionExit(Collision collision)
    {
        base.OnCollisionExit(collision);
    }

    public override void OnCollisionStay(Collision collision)
    {
        base.OnCollisionStay(collision);
    }

    public override void OnTriggerEnter(Collider Collider)
    {
        base.OnTriggerEnter(Collider);
    }

    public override void OnTriggerExit(Collider Collider)
    {
        base.OnTriggerExit(Collider);
    }

    public override void OnTriggerStay(Collider Collider)
    {
        base.OnTriggerStay(Collider);
    }
}
