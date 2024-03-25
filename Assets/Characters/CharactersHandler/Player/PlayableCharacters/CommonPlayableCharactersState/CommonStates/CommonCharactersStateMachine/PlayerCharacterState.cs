using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterState : IState
{
    protected PlayableCharacterStateMachine playableCharacterState { get; }

    public PlayerCharacterState(PlayableCharacterStateMachine pcs)
    {
        playableCharacterState = pcs;
    }

    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
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
}
