using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerElementalState : IState
{
    protected PlayableCharacterStateMachine playableCharacterStateMachine { get; }

    public PlayerElementalState(PlayableCharacterStateMachine pcs)
    {
        playableCharacterStateMachine = pcs;
    }

    public virtual void Enter()
    {
        SubscribeInputs();
    }

    public virtual void Exit()
    {
        UnsubscribeInputs();
    }

    public virtual void SubscribeInputs()
    {
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.performed += ElementalSkill_performed;
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.started += ElementalSkill_started;
    }

    public virtual void UnsubscribeInputs()
    {
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.performed -= ElementalSkill_performed;
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.started -= ElementalSkill_started;
    }

    protected virtual void ElementalSkill_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
    }

    protected virtual void ElementalSkill_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {

    }

    public virtual void FixedUpdate()
    {
    }

    public virtual void LateUpdate()
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

    public virtual void Update()
    {
    }

    protected void StartAnimation(string parameter)
    {
        Characters.StartAnimation(playableCharacterStateMachine.characters.Animator, parameter);
    }

    protected void SetAnimationTrigger(string parameter)
    {
        Characters.SetAnimationTrigger(playableCharacterStateMachine.characters.Animator, parameter);
    }
    protected void StopAnimation(string parameter)
    {
        Characters.StopAnimation(playableCharacterStateMachine.characters.Animator, parameter);
    }

    public void UpdateTargetRotationData(float data)
    {
        playableCharacterStateMachine.playerStateMachine.UpdateTargetRotationData(data);
    }

    public void SmoothRotateToTargetRotation()
    {
        playableCharacterStateMachine.playerStateMachine.SmoothRotateToTargetRotation();
    }
}
