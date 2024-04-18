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

    }

    public virtual void Exit()
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

    public virtual void SubscribeInputs()
    {

    }

    public virtual void UnsubscribeInputs()
    {

    }

    public virtual void Update()
    {

    }

    protected void StartAnimation(string parameter)
    {
        Characters.StartAnimation(characterStateMachine.characters.Animator, parameter);
    }

    protected void SetAnimationTrigger(string parameter)
    {
        Characters.SetAnimationTrigger(characterStateMachine.characters.Animator, parameter);
    }
    protected void StopAnimation(string parameter)
    {
        Characters.StopAnimation(characterStateMachine.characters.Animator, parameter);
    }
}
