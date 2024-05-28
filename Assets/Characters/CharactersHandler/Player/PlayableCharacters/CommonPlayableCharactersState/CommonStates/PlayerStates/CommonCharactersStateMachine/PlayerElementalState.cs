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
        OnEnable();
    }

    public virtual void Exit()
    {
        OnDisable();
    }

    public virtual void OnEnable()
    {
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.performed += ElementalSkill_performed;
        playableCharacterStateMachine.player.PlayerController.playerInputAction.ElementalSkill.started += ElementalSkill_started;
    }

    public virtual void OnDisable()
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

    protected PlayableCharacters playableCharacters
    {
        get
        {
            return playableCharacterStateMachine.playableCharacters;
        }
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
        playableCharacterStateMachine.characterReuseableData.Update();
    }

    public void StartAnimation(string parameter)
    {
        playableCharacterStateMachine.StartAnimation(parameter);
    }

    public void SetAnimationTrigger(string parameter)
    {
        playableCharacterStateMachine.SetAnimationTrigger(parameter);
    }
    public void StopAnimation(string parameter)
    {
        playableCharacterStateMachine.StopAnimation(parameter);
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
