using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineManager<T> where T : IState
{
    public T currentStates { get; private set; }

    public void Update()
    {
        if (currentStates != null)
            currentStates.Update();
    }
    public void FixedUpdate()
    {
        if (currentStates != null)
            currentStates.FixedUpdate();
    }

    public void OnAnimationTransition()
    {
        if (currentStates != null)
            currentStates.OnAnimationTransition();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (currentStates != null)
            currentStates.OnCollisionEnter(collision);
    }

    public void OnCollisionExit(Collision collision)
    {
        if (currentStates != null)
            currentStates.OnCollisionExit(collision);
    }

    public void OnCollisionStay(Collision collision)
    {
        if (currentStates != null)
            currentStates.OnCollisionStay(collision);
    }
    public void OnTriggerEnter(Collider Collider)
    {
        if (currentStates != null)
            currentStates.OnTriggerEnter(Collider);
    }

    public void OnTriggerExit(Collider Collider)
    {
        if (currentStates != null)
            currentStates.OnTriggerExit(Collider);
    }

    public void OnTriggerStay(Collider Collider)
    {
        if (currentStates != null)
            currentStates.OnTriggerStay(Collider);
    }

    public void ChangeState(T newState)
    {
        if (currentStates != null)
            currentStates.Exit();

        currentStates = newState;

        currentStates.Enter();
    }
}
