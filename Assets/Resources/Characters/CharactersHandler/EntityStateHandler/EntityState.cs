using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityState : IState
{
    protected CharacterStateMachine characterStateMachine;

    public EntityState(CharacterStateMachine characterStateMachine)
    {
        this.characterStateMachine = characterStateMachine;
    }

    public virtual void Enter()
    {
        OnEnable();
    }

    public virtual void Exit()
    {
        OnDisable();
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

    public virtual void UpdateTargetRotationData(float angle)
    {
    }
    public virtual void SmoothRotateToTargetRotation()
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

    public void StartAnimation(string parameter)
    {
        characterStateMachine.StartAnimation(parameter);
    }

    public virtual void OnEnable()
    {
    }

    public virtual void OnDisable()
    {
    }


    public void SetAnimationTrigger(string parameter)
    {
        characterStateMachine.SetAnimationTrigger(parameter);
    }
    public void StopAnimation(string parameter)
    {
        characterStateMachine.StopAnimation(parameter);
    }
}
