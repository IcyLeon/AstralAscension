using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterState : IState
{
    protected PlayableCharacterStateMachine playableCharacterStateMachine { get; }

    public PlayerCharacterState(PlayableCharacterStateMachine pcs)
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

    protected virtual void SubscribeInputs()
    {

    }
    protected virtual void UnsubscribeInputs()
    {

    }

    public virtual void FixedUpdate()
    {
    }
    public virtual void LateUpdate()
    {
    }

    public virtual void Update()
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
}
