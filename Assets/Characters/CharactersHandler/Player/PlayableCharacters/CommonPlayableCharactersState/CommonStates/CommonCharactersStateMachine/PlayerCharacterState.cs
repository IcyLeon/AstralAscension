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
}
