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

    protected override void OnDamageHit(object source)
    {
        if (playableCharacterStateMachine.playerStateMachine.GetCurrentState() is PlayerAirborneState)
            return;

        base.OnDamageHit(source);
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
        SubscribeInputs();
    }

    public override void Exit()
    {
        base.Exit();
        UnsubscribeInputs();
    }

    protected virtual bool CanTransitToElementalState()
    {
        return playableCharacterStateMachine.playerStateMachine.IsInState<PlayerGroundedState>();
    }

    public override void SubscribeInputs()
    {
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.performed += ElementalSkill_performed;
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.started += ElementalSkill_started;
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalBurst.performed += ElementalBurst_performed;
    }

    public override void UnsubscribeInputs()
    {
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.performed -= ElementalSkill_performed;
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.started -= ElementalSkill_started;
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalBurst.performed -= ElementalBurst_performed;
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
